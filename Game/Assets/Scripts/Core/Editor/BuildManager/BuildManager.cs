using System.Collections.Generic;

namespace Core.BuildManager
{
    public abstract class BuildManager
    {
        protected readonly List<BuildParameter> _allBuildParameter = new List<BuildParameter>();
        private readonly List<BuildThread> _allBuildThread = new List<BuildThread>();
        private protected List<BuildStep> _allBuildStep = new List<BuildStep>();
    }
}