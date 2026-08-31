using GameStoreApi.Dtos;
using GameStoreApi.Data;

namespace GameStoreApi.Endpoints;

public static class GamesEndPoints



{

    const string GetGameEndpointName = "GetGame";
    public static readonly string GetGameEndpointRoute = "/{id}";
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        group.MapGet("/", GetAllGames);
        group.MapGet(GetGameEndpointRoute, GetGameById)
             .WithName(GetGameEndpointName);
        return group;
    }
    public static IResult GetAllGames() => Results.Ok(GameData.Games);

    public static IResult GetGameById(int id)
    {
        var game = GameData.Games.FirstOrDefault(g => g.Id == id);
        return game is null ? Results.NotFound() : Results.Ok(game);
    }

}