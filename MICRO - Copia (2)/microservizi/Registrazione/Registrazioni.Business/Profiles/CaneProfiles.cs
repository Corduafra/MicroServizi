
using Registrazioni.Repository.Model;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Registrazioni.Shared;



namespace Registrazioni.Business.Profiles;


/// <summary>
/// Marker per <see cref="AutoMapper"/>.
/// </summary>
public sealed class AssemblyMarker
{
    AssemblyMarker() { }
}

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class InputFileProfile : Profile
{
    public InputFileProfile()
    {
        CreateMap<VotazioneDto, Votazione>();
        CreateMap<Votazione, VotazioneDto>();
    }
}
