function applyAccessibilityMetadata() {
  const landmarks = [
    ["#autocollapse", "Hauptnavigation / Main navigation"],
    ["#breadcrumb", "Brotkrumennavigation / Breadcrumb navigation"],
    ["#toc", "Inhaltsverzeichnis / Table of contents"],
    ["#affix", "Auf dieser Seite / On this page"],
  ];

  for (const [selector, label] of landmarks) {
    document.querySelector(selector)?.setAttribute("aria-label", label);
  }

  for (const code of document.querySelectorAll(".codewrapper pre code")) {
    code.setAttribute("tabindex", "0");
  }

  document
    .querySelector(".navbar-brand")
    ?.setAttribute("aria-label", "TinyPl0 Startseite / TinyPl0 home page");

  for (const heading of document.querySelectorAll("#affix > h5")) {
    const accessibleHeading = document.createElement("h2");
    accessibleHeading.className = heading.className;
    accessibleHeading.append(...heading.childNodes);
    heading.replaceWith(accessibleHeading);
  }

  const themeToggle = document.querySelector("a.dropdown-toggle[title]");
  if (themeToggle) {
    themeToggle.setAttribute("role", "button");
    themeToggle.setAttribute("tabindex", "0");
    themeToggle.setAttribute(
      "aria-label",
      "Darstellung wechseln / Change theme",
    );
  }
}

export default {
  start() {
    applyAccessibilityMetadata();

    // Die moderne DocFX-Vorlage erzeugt Teile der Navigation erst nach dem Start.
    // The modern DocFX template creates parts of the navigation after startup.
    const observer = new MutationObserver(applyAccessibilityMetadata);
    observer.observe(document.body, { childList: true, subtree: true });
  },
};
