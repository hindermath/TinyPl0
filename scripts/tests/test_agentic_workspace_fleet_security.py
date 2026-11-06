import importlib.util
import pathlib
import tempfile
import unittest
import subprocess


MODULE_PATH = pathlib.Path(__file__).parents[1] / "lib" / "agentic_workspace_fleet.py"
SPEC = importlib.util.spec_from_file_location("agentic_workspace_fleet", MODULE_PATH)
assert SPEC is not None and SPEC.loader is not None
fleet = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(fleet)


class AgenticWorkspaceFleetSecurityTests(unittest.TestCase):
    def test_real_home_relative_target_is_accepted(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            home = pathlib.Path(temporary).resolve()
            (home / "Projects").mkdir()

            target = fleet.resolve_home_target(
                home,
                fleet.validate_relative_path("Projects/TinyPl0"),
            )

            self.assertEqual(home / "Projects" / "TinyPl0", target)

    def test_symlinked_collection_parent_is_rejected_for_child(self) -> None:
        with tempfile.TemporaryDirectory() as temporary:
            root = pathlib.Path(temporary)
            home = root / "home"
            external = root / "external"
            home.mkdir()
            external.mkdir()
            (home / "Projects").symlink_to(external, target_is_directory=True)

            with self.assertRaisesRegex(
                fleet.ContractError,
                "symlinked path component|outside HOME",
            ):
                fleet.resolve_home_target(
                    home.resolve(),
                    fleet.validate_relative_path("Projects/TinyPl0"),
                )

    def test_delegated_network_git_never_falls_back_to_host(self) -> None:
        class FakeContexts:
            def __init__(self) -> None:
                self.calls: list[tuple[pathlib.Path, tuple[str, ...]]] = []

            @staticmethod
            def mapping(repository: pathlib.Path) -> tuple[str, str]:
                return ("/container", str(repository))

            def git(
                self,
                repository: pathlib.Path,
                arguments: tuple[str, ...],
            ) -> subprocess.CompletedProcess[str]:
                self.calls.append((repository, arguments))
                return subprocess.CompletedProcess(["git", *arguments], 0, "", "")

        contexts = FakeContexts()
        previous = fleet.EXECUTION_CONTEXTS
        fleet.EXECUTION_CONTEXTS = contexts
        try:
            result, attempts, _ = fleet.run_git_network(
                pathlib.Path("/delegated/repository"),
                "fetch",
                "--prune",
            )
        finally:
            fleet.EXECUTION_CONTEXTS = previous

        self.assertEqual(0, result.returncode)
        self.assertEqual(1, attempts)
        self.assertEqual(
            [(pathlib.Path("/delegated/repository"), ("fetch", "--prune"))],
            contexts.calls,
        )


if __name__ == "__main__":
    unittest.main()
