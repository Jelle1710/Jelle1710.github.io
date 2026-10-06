const projecten = [
  {
    titel: "Hotelsimulatie",
    categorie: "school",
    beschrijving: "Een groepsproject waarin gasten op basis van een JSON-bestand en scenario's over kamers worden verdeeld.",
    technologie: ["C#", "JSON"],
  },
  {
    titel: "Portfolio website",
    categorie: "web",
    beschrijving: "Mijn eigen portfolio met meerdere pagina's, navigatie, blogintegratie en een contactformulier.",
    technologie: ["HTML", "CSS", "JavaScript"],
  },
  {
    titel: "Game concept",
    categorie: "game",
    beschrijving: "Een eerste concept voor een game die later verder uitgewerkt kan worden.",
    technologie: ["Design", "Concept"],
  },
];

const projectGrid = document.querySelector("#project-grid");
const projectStatus = document.querySelector("#project-status");
const zoekInput = document.querySelector("#project-zoek");
const categorieSelect = document.querySelector("#project-categorie");

function renderProjects() {
  const zoekwaarde = zoekInput.value.trim().toLowerCase();
  const categorie = categorieSelect.value;

  const filtered = projecten.filter((project) => {
    const pastCategorie = categorie === "alle" || project.categorie === categorie;
    const zoekMatch =
      project.titel.toLowerCase().includes(zoekwaarde) ||
      project.beschrijving.toLowerCase().includes(zoekwaarde);
    return pastCategorie && zoekMatch;
  });

  projectStatus.textContent = `${filtered.length} project(en) gevonden.`;

  projectGrid.innerHTML = filtered
    .map(
      (project) => `
        <article class="project-card">
          <h2>${project.titel}</h2>
          <p><strong>Categorie:</strong> ${project.categorie}</p>
          <p>${project.beschrijving}</p>
          <p><strong>Technologie:</strong> ${project.technologie.join(", ")}</p>
        </article>
      `
    )
    .join("");
}

zoekInput.addEventListener("input", renderProjects);
categorieSelect.addEventListener("change", renderProjects);

renderProjects();