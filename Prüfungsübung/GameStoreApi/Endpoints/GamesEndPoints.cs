using GameStoreApi.Dtos;
using GameStoreApi.Data;

namespace GameStoreApi.Endpoints;

public static class GamesEndPoints



{

    const string GetGameEndpointName = "GetGame";
    private static readonly string GetGameEndpointRoute = "/{id}";
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        group.MapGet("/", () => GameData.Games);
        group.MapGet(GetGameEndpointRoute, (int id) => GameData.Games.FirstOrDefault(g => g.Id == id))
             .WithName(GetGameEndpointName);
        return group;
    }
    private static IResult GetAllGames() => Results.Ok(GameData.Games);

    private static IResult GetGameById(int id)
    {
        var game = GameData.Games.FirstOrDefault(g => g.Id == id);
        return game is null ? Results.NotFound() : Results.Ok(game);
    }

}