import AxeBuilder from '@axe-core/playwright';
import { expect, test } from '@playwright/test';
import { execFile } from 'node:child_process';
import { readFile, stat, writeFile } from 'node:fs/promises';
import { createServer } from 'node:http';
import { tmpdir } from 'node:os';
import { extname, resolve, sep } from 'node:path';
import { fileURLToPath } from 'node:url';
import { promisify } from 'node:util';

const baseUrl = process.env.DOCS_BASE_URL ?? 'http://127.0.0.1:8080';
const pages = [
  ['Startseite', '/index.html'],
  ['VirtualMachineOptions API', '/api/Pl0.Vm.VirtualMachineOptions.html'],
  ['VirtualMachine API', '/api/Pl0.Vm.VirtualMachine.html'],
  ['VmExecutionSession API', '/api/Pl0.Vm.VmExecutionSession.html'],
];
const results = [];
const execFileAsync = promisify(execFile);
const repoRoot = fileURLToPath(new URL('../..', import.meta.url));
const siteRoot = resolve(repoRoot, '_site');
const manageServer = process.env.MANAGE_DOCS_SERVER === '1';
let serverProcess;

const contentTypes = new Map([
  ['.css', 'text/css; charset=utf-8'],
  ['.html', 'text/html; charset=utf-8'],
  ['.ico', 'image/x-icon'],
  ['.js', 'text/javascript; charset=utf-8'],
  ['.json', 'application/json; charset=utf-8'],
  ['.png', 'image/png'],
  ['.svg', 'image/svg+xml; charset=utf-8'],
  ['.txt', 'text/plain; charset=utf-8'],
]);

async function stopServer() {
  if (!serverProcess?.listening) {
    return;
  }

  await new Promise((resolveClose, rejectClose) => {
    serverProcess.close((error) => error ? rejectClose(error) : resolveClose());
  });
}

test.beforeAll(async () => {
  if (!manageServer) {
    return;
  }

  serverProcess = createServer(async (request, response) => {
    try {
      if (!['GET', 'HEAD'].includes(request.method ?? '')) {
        response.writeHead(405, { Allow: 'GET, HEAD' });
        response.end();
        return;
      }

      const pathname = decodeURIComponent(new URL(request.url ?? '/', baseUrl).pathname);
      const relativePath = pathname === '/' ? 'index.html' : pathname.slice(1);
      const filePath = resolve(siteRoot, relativePath);
      if (filePath !== siteRoot && !filePath.startsWith(`${siteRoot}${sep}`)) {
        response.writeHead(404);
        response.end();
        return;
      }

      const fileInfo = await stat(filePath);
      if (!fileInfo.isFile()) {
        response.writeHead(404);
        response.end();
        return;
      }

      const body = await readFile(filePath);
      response.writeHead(200, {
        'Content-Length': body.length,
        'Content-Type': contentTypes.get(extname(filePath)) ?? 'application/octet-stream',
      });
      response.end(request.method === 'HEAD' ? undefined : body);
    } catch {
      response.writeHead(404);
      response.end();
    }
  });

  await new Promise((resolveListen, rejectListen) => {
    serverProcess.once('error', rejectListen);
    serverProcess.listen(8080, '127.0.0.1', resolveListen);
  });
});

test.afterAll(async () => {
  try {
    const outputPath = process.env.AXE_RESULTS_PATH;
    if (outputPath) {
      await writeFile(outputPath, `${JSON.stringify({ pages: results }, null, 2)}\n`, 'utf8');
    }

    if (manageServer) {
      const dumps = [
        ['api/Pl0.Vm.VirtualMachineOptions.html', resolve(tmpdir(), 'tinypl0-004-virtual-machine-options.txt'), ['VirtualMachineOptions', 'InstructionBudget', 'StackSize']],
        ['api/Pl0.Vm.VirtualMachine.html', resolve(tmpdir(), 'tinypl0-004-virtual-machine.txt'), ['VirtualMachine', 'Run', 'CultureNotFoundException']],
        ['api/Pl0.Vm.VmExecutionSession.html', resolve(tmpdir(), 'tinypl0-006-vm-execution-session.txt'), ['VmExecutionSession', 'ExecuteNext', 'CancellationToken']],
      ];
      for (const [path, outputPath, tokens] of dumps) {
        const { stdout } = await execFileAsync('lynx', ['-dump', '-nolist', `${baseUrl}/${path}`]);
        for (const token of tokens) {
          expect(stdout, `${path} must contain ${token}`).toContain(token);
        }
        await writeFile(outputPath, stdout, 'utf8');
      }
    }
  } finally {
    await stopServer();
  }
});

for (const [name, path] of pages) {
  test(`${name} erfüllt den axe-Vertrag / meets the axe contract`, async ({ page }) => {
    const response = await page.goto(`${baseUrl}${path}`, { waitUntil: 'networkidle' });
    expect(response?.ok(), `${path} must return a successful response`).toBeTruthy();

    const result = await new AxeBuilder({ page }).analyze();
    results.push({
      name,
      path,
      url: `${baseUrl}${path}`,
      violations: result.violations,
    });
    const severe = result.violations.filter(({ impact }) =>
      impact === 'critical' || impact === 'serious');

    expect(severe, JSON.stringify(severe, null, 2)).toEqual([]);
    expect(result.violations, JSON.stringify(result.violations, null, 2)).toEqual([]);
  });
}
