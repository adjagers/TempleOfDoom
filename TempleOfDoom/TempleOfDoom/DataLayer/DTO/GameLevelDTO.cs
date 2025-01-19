using TempleOfDoom.Interfaces;
namespace TempleOfDoom.DataLayer.DTO
{
    public record GameLevelDTO(
        List<RoomDTO> Rooms,
        List<ConnectionDTO> Connections,
        PlayerDTO Player
    ) : IDTO;
}
