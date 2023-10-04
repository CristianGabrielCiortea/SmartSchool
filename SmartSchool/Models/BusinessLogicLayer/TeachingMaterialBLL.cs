using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class TeachingMaterialBLL
    {
        private TeachingMaterialDAL _teachingMaterialDAL = new TeachingMaterialDAL();

        public void AddTeachingMaterial(TeachingMaterial teachingMaterial)
        {
            _teachingMaterialDAL.AddTeachingMaterial(teachingMaterial);
        }

        public void DeleteTeachingMaterial(TeachingMaterial teachingMaterial)
        {
            _teachingMaterialDAL.DeleteTeachingMaterial(teachingMaterial);
        }

        public ObservableCollection<TeachingMaterial> GetTeachingMaterials()
        {
            return _teachingMaterialDAL.GetAllTeachingMaterials();
        }
    }
}