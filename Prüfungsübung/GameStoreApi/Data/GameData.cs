using GameStoreApi.Dtos;

namespace GameStoreApi.Data;

public static class GameData
{
    public static readonly IReadOnlyList<GamesDto> Games = new List<GamesDto>
    {
        new GamesDto { Id = 1, Name = "Game 1", Genre = "Action", Price = 59.99m },
        new GamesDto { Id = 2, Name = "Game 2", Genre = "Adventure", Price = 49.99m },
        new GamesDto { Id = 3, Name = "Game 3", Genre = "RPG", Price = 39.99m }
    };
}