using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TkoUtilities.EventBus;

namespace InflatedPufferfish.Events;

internal record AnimationFinished(string animationName): IEvent;
