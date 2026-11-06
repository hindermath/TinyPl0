import json
import pathlib
import shutil
import subprocess
import tempfile
import unittest


REPOSITORY = pathlib.Path(__file__).parents[2]


class RegistryProbeAuthorityTests(unittest.TestCase):
    def test_bash_rejects_alternate_executable_registry_without_authority(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            registry = pathlib.Path(temporary) / "npm-tools.json"
            registry.write_text(json.dumps({"tools": []}), encoding="utf-8")

            result = subprocess.run(
                [
                    "bash",
                    str(REPOSITORY / "scripts/maintain-agentic-brew-apps.sh"),
                    "--compare-only",
                    "--npm-agent-registry",
                    str(registry),
                ],
                cwd=REPOSITORY,
                capture_output=True,
                text=True,
                check=False,
            )

            self.assertEqual(2, result.returncode)
            self.assertIn("--allow-external-registry-probes", result.stderr)

    @unittest.skipUnless(shutil.which("pwsh"), "PowerShell 7 is required.")
    def test_powershell_rejects_alternate_executable_registry_without_authority(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            registry = pathlib.Path(temporary) / "npm-tools.json"
            registry.write_text(json.dumps({"tools": []}), encoding="utf-8")

            result = subprocess.run(
                [
                    "pwsh",
                    "-NoProfile",
                    "-File",
                    str(REPOSITORY / "scripts/maintain-agentic-winget-apps.ps1"),
                    "-CompareOnly",
                    "-NpmAgentRegistry",
                    str(registry),
                ],
                cwd=REPOSITORY,
                capture_output=True,
                text=True,
                check=False,
            )

            self.assertNotEqual(0, result.returncode)
            self.assertIn("AllowExternalRegistryProbes", result.stderr)


if __name__ == "__main__":
    unittest.main()
