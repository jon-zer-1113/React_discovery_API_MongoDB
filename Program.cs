using ReactMongoDB_API_01.Models;
using ReactMongoDB_API_01.Services;

////////////////////////////////////////////////////////////////////////////////////////////////////
var builder = WebApplication.CreateBuilder(args);
// Construit et initialise l'objet "builder" 

builder.Services.Configure<ElementDatabaseSettings>(
    builder.Configuration.GetSection("MaDatabaseMongo"));
// J'utilise les valeurs de la section "MaDatabaseMongo" de "appsettings.json" pour configurer les options de "ElementDatabaseSettings".

builder.Services.AddSingleton<ElementsService>();
/*
Ici on ajoute "ElementService" au conteneur de services avec une durée de vie singleton (Spécifie qu’une seule instance du service sera créée).
- De plus : garantit que toutes les parties de l'application qui nécessitent ce service travaillent avec la même instance, ce qui peut être important pour maintenir la cohérence des données et l'état de l'application.
- le + : meilleures performances de l'app !
*/

builder.Services.AddControllers();
// Ajoute le service des contrôleurs à l'application.
// Les contrôleurs sont responsables de la gestion des requêtes HTTP et de la génération des réponses.

//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
// Ajoute le service SwaggerGen pour la génération de la documentation Swagger/OpenAPI, et tester l'API en question.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
// Configure CORS, pour permettre à l'API d'accepter des requêtes provenant de domaines différents (en gros, pour autoriser mon app REACT à requeter sans soucis...).
// La politique CORS configurée ici permet à n'importe quelle origine d'envoyer des requêtes, d'utiliser n'importe quelle méthode HTTP et d'inclure n'importe quel en-tête.

////////////////////////////////////////////////////////////////////////////////////////////////////
var app = builder.Build();
/*
Construit et initialise l'objet "app", qui représente l'application web complète.
Ensuite on configure les middlewares (app. ...) dans le pipeline de requêtes de l'application que l'on a construit.
Ce pipeline est une série d'étapes (middlewares) que les requêtes HTTP doivent traverser pour être traitées...
*/

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Ces lignes sont exécutées uniquement lorsque l'application est en mode développement.
// Elles activent la prise en charge de Swagger et configurent l'interface utilisateur Swagger pour la documentation interactive de l'API.

app.UseHttpsRedirection();
// Cela redirige toutes les requêtes HTTP vers HTTPS pour assurer une communication sécurisée.

app.UseRouting();
// Configure le routage des requêtes, en déterminant quel contrôleur et quelle action doivent être exécutés en fonction de l'URL demandée.

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
