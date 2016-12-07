﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using ShaderTools.VisualStudio.Core.Tagging.Classification;
using ShaderTools.VisualStudio.Core.Util;

namespace ShaderTools.VisualStudio.ShaderLab.Tagging.Classification
{
    [Export]
    internal sealed class ClassificationColorManager : ClassificationColorManagerBase
    {
        [ImportingConstructor]
        public ClassificationColorManager(ThemeManager themeManager, IClassificationFormatMapService classificationFormatMapService, IClassificationTypeRegistryService classificationTypeRegistryService)
            : base(themeManager, classificationFormatMapService, classificationTypeRegistryService)
        {
        }

        protected override Dictionary<string, Color> CreateLightAndBlueColors()
        {
            return new Dictionary<string, Color>
            {
                { SemanticClassificationMetadata.PunctuationClassificationTypeName, Colors.Black }
            };
        }

        protected override Dictionary<string, Color> CreateDarkColors()
        {
            return new Dictionary<string, Color>
            {
                { SemanticClassificationMetadata.PunctuationClassificationTypeName, Colors.White }
            };
        }
    }
}