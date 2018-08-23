using System;
using System.Linq;
using VEdit.Common;

namespace VEdit.Editor
{
    public class GraphNode : Node
    {
        public Guid LinkedGraphId { get; }

        private IGraphProvider _provider;

        private Graph _linkedGraph;
        public Graph LinkedGraph
        {
            get
            {
                if (_linkedGraph == null)
                {
                    _linkedGraph = _provider.Get(LinkedGraphId);
                }
                return _linkedGraph;
            }
        }

        public GraphNode(Graph root, Guid templateId) : base(root, templateId)
        {
            Id = templateId;

            var cmdProvider = ServiceProvider.Get<ICommandProvider>();
            _provider = ServiceProvider.Get<IGraphProvider>();

            Actions.Add("Go to Graph", cmdProvider.Create(GoToGraph));

            LinkedGraphId = templateId;
        }

        public void GoToGraph()
        {
            if (LinkedGraphId != Graph.Id)
            {
                _provider.OpenInEditor(LinkedGraphId);
            }
        }
    }
}
