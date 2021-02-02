namespace EMP.Common.Data
{
    public static class Validation
    {
        public static bool isNumeric(string value){
            return int.TryParse(value, out int n);
        }
    }
}