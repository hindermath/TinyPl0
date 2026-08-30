import AxeBuilder from '@axe-core/playwright';
import { expect, test } from '@playwright/test';

const baseUrl = process.env.DOCS_BASE_URL ?? 'http://127.0.0.1:8080';
const pages = [
  ['Startseite', '/index.html'],
  ['VirtualMachineOptions API', '/api/Pl0.Vm.VirtualMachineOptions.html'],
  ['VirtualMachine API', '/api/Pl0.Vm.VirtualMachine.html'],
];

for (const [name, path] of pages) {
  test(`${name} erfüllt den axe-Vertrag / meets the axe contract`, async ({ page }) => {
    const response = await page.goto(`${baseUrl}${path}`, { waitUntil: 'networkidle' });
    expect(response?.ok(), `${path} must return a successful response`).toBeTruthy();

    const result = await new AxeBuilder({ page }).analyze();
    const severe = result.violations.filter(({ impact }) =>
      impact === 'critical' || impact === 'serious');

    expect(severe, JSON.stringify(severe, null, 2)).toEqual([]);
    expect(result.violations, JSON.stringify(result.violations, null, 2)).toEqual([]);
  });
}
