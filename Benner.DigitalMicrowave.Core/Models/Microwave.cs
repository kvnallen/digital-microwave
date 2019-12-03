﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Benner.DigitalMicrowave.Core.Events;
using Benner.DigitalMicrowave.Core.Extensions;

namespace Benner.DigitalMicrowave.Core.Models
{
    public class Microwave
    {
        public MicrowaveOptions Options { get; }
        public string OriginalText { get; }
        public MicrowaveState State { get; private set; }
        private int _currentSecond = 0;

        public Microwave(MicrowaveOptions options)
        {
            Options = options;
            OriginalText = Options.Text;
            State = MicrowaveState.Idle;
        }

        public async Task<string> Warm(IEnumerable<IMicrowaveNotifier> notifiers)
        {
            State = MicrowaveState.Running;
            var text = new StringBuilder(Options.Text);

            while (_currentSecond < Options.Time)
            {
                if (State == MicrowaveState.Cancelled)
                {
                    await notifiers.NotifyCancellation(text.ToString());
                    break;
                }

                if (State == MicrowaveState.Idle)
                {
                    await notifiers.NotifyPause(text.ToString(), _currentSecond);
                    break;
                }

                text.Append(Options.HeatingCharacter.Repeat(Options.Power));

                await notifiers.NotifyAll(text.ToString());
                await Task.Delay(TimeSpan.FromSeconds(1));
                _currentSecond++;
            }

            if (State == MicrowaveState.Running)
            {
                await notifiers.NotifyFinished(text.ToString());
            }

            return text.ToString();
        }

        public void Cancel()
        {
            State = MicrowaveState.Cancelled;
        }

        public void Pause()
        {
            State = MicrowaveState.Idle;
        }
    }
}