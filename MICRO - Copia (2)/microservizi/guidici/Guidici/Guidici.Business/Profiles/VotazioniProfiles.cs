using AutoMapper;
using Guidici.Repository.Model;
using Guidici.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidici.Business.Profiles
{

    /// <summary>
    /// Marker per <see cref="AutoMapper"/>.
    /// </summary>
    public sealed class AssemblyMarker
    {
        AssemblyMarker() { }
    }
   

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
    public class VotazioneProfile : Profile
    {
        public VotazioneProfile()
        {
            // Mapping bidirezionale tra VotazioneDto e Votazione
            CreateMap<VotazioneDto, VotazioneKafka>().ReverseMap();
        }
    }


}
