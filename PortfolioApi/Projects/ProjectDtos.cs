namespace PortfolioApi.Projects;

public record ProjectInput(
    string Titel,
    string Beschrijving,
    string Categorie,
    string? GitHubUrl,
    DateTime Datum);

public record ProjectResponse(
    int Id,
    string Titel,
    string Beschrijving,
    string Categorie,
    string? GitHubUrl,
    DateTime Datum);