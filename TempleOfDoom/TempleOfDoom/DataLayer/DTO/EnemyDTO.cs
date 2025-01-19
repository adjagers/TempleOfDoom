namespace TempleOfDoom.DataLayer.DTO
{
    public record EnemyDTO(
        string Type,
        int X,
        int Y,
        int MinX,
        int MinY,
        int MaxX,
        int MaxY
    );
}