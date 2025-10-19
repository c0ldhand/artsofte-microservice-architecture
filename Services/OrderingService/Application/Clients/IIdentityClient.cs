using Application.DTO;


namespace Application.Clients
{
    public interface IIdentityClient
    {
        Task<UserDTO?> GetUserByIdAsync(Guid id, CancellationToken ct = default);
    }
}
