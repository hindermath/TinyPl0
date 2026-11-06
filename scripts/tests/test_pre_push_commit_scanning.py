import os
import pathlib
import shutil
import subprocess
import tempfile
import unittest


HOOK = pathlib.Path(__file__).parents[1] / "hooks" / "pre-push"


class PrePushCommitScanningTests(unittest.TestCase):
    def test_regex_fallback_scans_commit_blob_not_worktree_file(self) -> None:
        self._assert_commit_blob_scanned("settings.txt")

    def test_regex_fallback_scans_newline_filename_from_nul_delimited_git_output(self) -> None:
        self._assert_commit_blob_scanned("safe\nname.txt")

    def _assert_commit_blob_scanned(self, file_name: str) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            root = pathlib.Path(temporary)
            repository = root / "repository"
            repository.mkdir()
            self._run(["git", "init", "-q"], repository)
            self._run(["git", "config", "user.name", "Security Fixture"], repository)
            self._run(["git", "config", "user.email", "fixture@example.invalid"], repository)
            tracked = repository / file_name
            tracked.write_text("safe=true\n", encoding="utf-8")
            self._run(["git", "add", "--", file_name], repository)
            self._run(["git", "commit", "-qm", "baseline"], repository)
            baseline = self._output(["git", "rev-parse", "HEAD"], repository)

            tracked.write_text("api_key=fixture-secret-value\n", encoding="utf-8")
            self._run(["git", "add", "--", file_name], repository)
            self._run(["git", "commit", "-qm", "secret commit"], repository)
            local = self._output(["git", "rev-parse", "HEAD"], repository)
            tracked.write_text("safe=true\n", encoding="utf-8")

            tool_bin = root / "bin"
            tool_bin.mkdir()
            for name in ("git", "grep", "head"):
                executable = shutil.which(name)
                assert executable is not None
                (tool_bin / name).symlink_to(executable)
            environment = os.environ.copy()
            environment["PATH"] = str(tool_bin)
            environment["HOME"] = str(root / "home")
            pathlib.Path(environment["HOME"]).mkdir()

            push_line = f"refs/heads/main {local} refs/heads/main {baseline}\n"
            result = subprocess.run(
                [shutil.which("bash") or "/bin/bash", str(HOOK)],
                cwd=repository,
                input=push_line,
                capture_output=True,
                text=True,
                env=environment,
                check=False,
            )

            self.assertEqual(2, result.returncode)
            self.assertIn(local, result.stderr)

    @staticmethod
    def _run(command: list[str], cwd: pathlib.Path) -> None:
        subprocess.run(command, cwd=cwd, check=True, capture_output=True, text=True)

    @staticmethod
    def _output(command: list[str], cwd: pathlib.Path) -> str:
        return subprocess.run(
            command,
            cwd=cwd,
            check=True,
            capture_output=True,
            text=True,
        ).stdout.strip()


if __name__ == "__main__":
    unittest.main()
