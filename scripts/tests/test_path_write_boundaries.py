import pathlib
import subprocess
import tempfile
import unittest


REPOSITORY = pathlib.Path(__file__).parents[2]


class PathWriteBoundaryTests(unittest.TestCase):
    def test_bash_homogeneity_patch_rejects_target_outside_repository(self) -> None:
        self._assert_homogeneity_rejects_outside_target(
            ["bash", str(REPOSITORY / "scripts/check-homogeneity.sh")],
            ["--apply-patch", "{patch}", "--yes", "{target}"],
        )

    def test_powershell_homogeneity_patch_rejects_target_outside_repository(self) -> None:
        self._assert_homogeneity_rejects_outside_target(
            ["pwsh", "-NoProfile", "-File", str(REPOSITORY / "scripts/check-homogeneity.ps1")],
            ["-ApplyPatch", "{patch}", "-Yes", "-TargetDir", "{target}"],
        )

    def test_feature_metadata_cannot_escape_repository_specs_tree(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            fixture = pathlib.Path(temporary)
            subprocess.run(["git", "init", "-q"], cwd=fixture, check=True)
            (fixture / ".specify").mkdir()
            (fixture / "specs").mkdir()
            (fixture / ".specify/feature.json").write_text(
                '{"feature_directory":"../outside"}\n',
                encoding="utf-8",
            )
            command = (
                f'source "{REPOSITORY}/.specify/scripts/bash/common.sh"; '
                'get_feature_paths'
            )
            result = subprocess.run(
                ["bash", "-c", command],
                cwd=fixture,
                capture_output=True,
                text=True,
                check=False,
            )

        self.assertNotEqual(0, result.returncode)
        self.assertIn("Unsafe feature directory", result.stderr)

    def test_explicit_feature_directory_remains_an_operator_override(self) -> None:
        command = (
            'source ".specify/scripts/bash/common.sh"; '
            'SPECIFY_FEATURE_DIRECTORY="/tmp/operator-feature" get_feature_paths'
        )

        result = subprocess.run(
            ["bash", "-c", command],
            cwd=REPOSITORY,
            capture_output=True,
            text=True,
            check=False,
        )

        self.assertEqual(0, result.returncode, result.stderr)
        self.assertIn("FEATURE_DIR=/tmp/operator-feature", result.stdout)

    def test_feature_metadata_rejects_symlinked_specs_tree(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            fixture = pathlib.Path(temporary)
            subprocess.run(["git", "init", "-q"], cwd=fixture, check=True)
            outside = fixture / "outside"
            outside.mkdir()
            (outside / "001-fixture").mkdir()
            (fixture / ".specify").mkdir()
            (fixture / "specs").symlink_to(outside, target_is_directory=True)
            (fixture / ".specify/feature.json").write_text(
                '{"feature_directory":"specs/001-fixture"}\n',
                encoding="utf-8",
            )
            command = (
                f'source "{REPOSITORY}/.specify/scripts/bash/common.sh"; '
                'get_feature_paths'
            )
            result = subprocess.run(
                ["bash", "-c", command],
                cwd=fixture,
                capture_output=True,
                text=True,
                check=False,
            )

        self.assertNotEqual(0, result.returncode)
        self.assertIn("Unsafe feature directory", result.stderr)

    def _assert_homogeneity_rejects_outside_target(
        self,
        command: list[str],
        arguments: list[str],
    ) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            root = pathlib.Path(temporary)
            target = root / "repository"
            target.mkdir()
            outside = root / "outside.txt"
            outside.write_text("original\n", encoding="utf-8")
            patch = root / "memory-patch.md"
            patch.write_text(
                f"### Target: {outside}\n+++ injected\n",
                encoding="utf-8",
            )
            resolved_arguments = [
                value.format(patch=patch, target=target)
                for value in arguments
            ]

            result = subprocess.run(
                [*command, *resolved_arguments],
                cwd=REPOSITORY,
                capture_output=True,
                text=True,
                check=False,
            )

            self.assertNotEqual(0, result.returncode)
            self.assertEqual("original\n", outside.read_text(encoding="utf-8"))
            self.assertIn("escapes selected repository", result.stderr)


if __name__ == "__main__":
    unittest.main()
