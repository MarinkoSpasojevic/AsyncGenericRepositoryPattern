using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Entities.ExtendedModels;
using Entities.Extensions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return FindAll()
                .OrderBy(ow => ow.Name);
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            var owners = await FindAllAsync();
            return owners.OrderBy(x => x.Name);
        }

        public Owner GetOwnerById(Guid ownerId)
        {
            return FindByCondition(owner => owner.Id.Equals(ownerId))
                    .DefaultIfEmpty(new Owner())
                    .FirstOrDefault();
        }

        public async Task<Owner> GetOwnerByIdAsync(Guid ownerId)
        {
            var owner = await FindByConditionAync(o => o.Id.Equals(ownerId));
            return owner.DefaultIfEmpty(new Owner())
                    .FirstOrDefault();
        }


        public OwnerExtended GetOwnerWithDetails(Guid ownerId)
        {
            return new OwnerExtended(GetOwnerById(ownerId))
            {
                Accounts = RepositoryContext.Accounts
                    .Where(a => a.OwnerId == ownerId)
            };
        }

        public async Task<OwnerExtended> GetOwnerWithDetailsAsync(Guid ownerId)
        {
            var owner = await GetOwnerByIdAsync(ownerId);

            return new OwnerExtended(owner)
            {
                Accounts = await RepositoryContext.Accounts
                    .Where(a => a.OwnerId == ownerId).ToListAsync()
            };
        }

        public void CreateOwner(Owner owner)
        {
            owner.Id = Guid.NewGuid();
            Create(owner);
            Save();
        }

        public async Task<int> CreateOwnerAsync(Owner owner)
        {
            owner.Id = Guid.NewGuid();
            Create(owner);
            return await SaveAsync();
        }

        public void UpdateOwner(Owner dbOwner, Owner owner)
        {
            dbOwner.Map(owner);
            Update(dbOwner);
            Save();
        }

        public async Task<int> UpdateOwnerAsync(Owner dbOwner, Owner owner)
        {
            dbOwner.Map(owner);
            Update(dbOwner);
            return await SaveAsync();
        }

        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
            Save();
        }

        public async Task<int> DeleteOwnerAsync(Owner owner)
        {
            Delete(owner);
            return await SaveAsync();
        }
    }
}