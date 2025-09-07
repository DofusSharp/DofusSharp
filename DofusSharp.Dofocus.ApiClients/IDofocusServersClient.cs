using DofusSharp.Dofocus.ApiClients.Models.Servers;

namespace DofusSharp.Dofocus.ApiClients;

public interface IDofocusServersClient
{
    Task<IReadOnlyCollection<DofocusServer>> GetServersAsync(CancellationToken cancellationToken = default);
}
