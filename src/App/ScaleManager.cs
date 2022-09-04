using Scale.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Scale
{
    internal class ScaleManager
    {
        List<Scale.Models.Scale> scales;

        internal ScaleManager()
        {
            var content = File.ReadAllText("scales.json");
            scales = JsonSerializer.Deserialize(content, typeof(List<Scale.Models.Scale>)) as List<Scale.Models.Scale>;
        }

        internal bool IsScaleSupported(string scaleId)
        {
            foreach (var scale in scales)
            {
                if (scale.Id.Equals(scaleId, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
