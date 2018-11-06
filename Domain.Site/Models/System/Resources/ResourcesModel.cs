using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Site.Models.System.RoleModulePermission
{
	public class ResourcesModel : IEquatable<ResourcesModel>
	{
		public int Id { get; set; }

        public int ResourcesId { get; set; }
        public int ResourcesType { get; set; }
        public int ModuleId { get; set; }

        public int SystemId { get; set; }
        public int? PermissionId { get; set; }

		public bool Equals(ResourcesModel rmpm)
		{

			//Check whether the compared object is null. 
			if (Object.ReferenceEquals(rmpm, null)) return false;

			//Check whether the compared object references the same data. 
			if (Object.ReferenceEquals(this, rmpm)) return true;

			//Check whether the objects properties are equal. 
			return ResourcesId.Equals(rmpm.ResourcesId) && ModuleId.Equals(rmpm.ModuleId) && PermissionId.Equals(rmpm.PermissionId)&& SystemId.Equals(rmpm.SystemId);
		}

		public override int GetHashCode()
		{
            //Get hash code for the ResourcesType field if it is not null. 
            int hashResourcesId = ResourcesId.GetHashCode();
            //Get hash code for the ResourcesType field if it is not null. 
            int hashResourcesType = ResourcesType.GetHashCode();

			//Get hash code for the ModuleId field. 
			int hashModuleId = ModuleId.GetHashCode();

            //Get hash code for the ModuleId field. 
            int hashSystemId = SystemId.GetHashCode();

            //Get hash code for the PermissionId field. 
            int hashPermissionId = PermissionId == null ? 0 : ModuleId.GetHashCode();

			//Calculate the hash code for the entire object. 
			return hashResourcesId^hashResourcesType ^ hashModuleId ^ hashSystemId ^ hashPermissionId;
		}
	}
}
