using Entities.ExtendedModels;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetAllOwners();
        Task<IEnumerable<Owner>> GetAllOwnersAsync();
        Owner GetOwnerById(Guid ownerId);
        Task<Owner> GetOwnerByIdAsync(Guid ownerId);
        OwnerExtended GetOwnerWithDetails(Guid ownerId);
        Task<OwnerExtended> GetOwnerWithDetailsAsync(Guid ownerId);
        void CreateOwner(Owner owner);
        Task CreateOwnerAsync(Owner owner);
        void UpdateOwner(Owner dbOwner, Owner owner);
        Task UpdateOwnerAsync(Owner dbOwner, Owner owner);
        void DeleteOwner(Owner owner);
        Task DeleteOwnerAsync(Owner owner);
    }
}
