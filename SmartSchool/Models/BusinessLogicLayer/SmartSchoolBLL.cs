namespace SmartSchool.Models.BusinessLogicLayer
{
    public class SmartSchoolBLL
    {
        public static UserBLL User { get; } = new UserBLL();

        public static SubjectBLL Subject { get; } = new SubjectBLL();

        public static SpecializationBLL Specialization { get; } = new SpecializationBLL();

        public static ClassBLL Class { get; } = new ClassBLL();

        public static StudentClassBLL StudentClass { get; } = new StudentClassBLL();

        public static TeacherClassBLL TeacherClass { get; } = new TeacherClassBLL();

        public static StudentSubjectBLL StudentSubject { get; } = new StudentSubjectBLL();

        public static TeacherSubjectBLL TeacherSubject { get; } = new TeacherSubjectBLL();

        public static SpecializationSubjectBLL SpecializationSubject { get; } = new SpecializationSubjectBLL();

        public static AbsenceBLL Absence { get; } = new AbsenceBLL();

        public static GradeBLL Grade { get; } = new GradeBLL();

        public static TeachingMaterialBLL TeachingMaterial { get; } = new TeachingMaterialBLL();
    }
}