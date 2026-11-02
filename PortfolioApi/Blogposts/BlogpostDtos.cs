namespace PortfolioApi.Blogposts;

public record BlogpostInput(
    string Titel,
    string Inhoud,
    DateTime Publicatiedatum);

public record BlogpostResponse(
    int Id,
    string Titel,
    string Inhoud,
    DateTime Publicatiedatum);