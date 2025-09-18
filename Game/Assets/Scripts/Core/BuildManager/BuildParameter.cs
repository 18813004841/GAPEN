using System.Collections.Generic;

namespace Core.BuildManager
{
    public class BuildParameter
    {
        private readonly string _key;
        public string Key => _key;

        private string _value;
        public string Value => _value;

        private readonly string _description;
        public string Description => _description;
        
        public BuildParameter(string key, string value, string description)
        {
            _key = key;
            _value = value;
            _description = description;
        }   
        
        public void SetValue(string value)
        {
            _value = value;
        }
    }
    
    public class GroovyBuildParameter : BuildParameter
    {
        private List<string> _valueRange = new List<string>();
        public List<string> ValueRange
        {
            get { return _valueRange; }
        }

        private Dictionary<BuildType, string> _defaultValue = new Dictionary<BuildType, string>();
        
        public GroovyBuildParameter(
            string key,
            string description, 
            string defaultValueDevelop,
            string defaultValueRelease,
            string defaultValueMaster,
            List<string> valueRange = null):base(key, defaultValueMaster, description)
        {
            _defaultValue.Add(BuildType.Develop, defaultValueDevelop);
            _defaultValue.Add(BuildType.Release, defaultValueRelease);
            _defaultValue.Add(BuildType.Master, defaultValueMaster);
            if (valueRange != null)
            {
                _valueRange = valueRange;
            }
        }

        public string GetDefaultValue(BuildType buildType)
        {
            return _defaultValue[buildType];
        }
    }
}