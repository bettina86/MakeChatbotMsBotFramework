﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Core.Services;
using Microsoft.Bot.Builder.Dialogs;
using Bot.Core.Dialogs.Flows;
using Microsoft.Bot.Connector;

namespace Bot.Core.Dialogs
{
    /// <summary>
    ///     Root <see cref="IDialog" /> for all conversations.
    /// </summary>
    [Serializable]
    public class RootDialog : IDialog
    {
        private readonly IDialogFactory _dialogFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RootDialog"/> class.
        /// </summary>
        public RootDialog(IDialogFactory dialogFactory)
        {
            _dialogFactory = dialogFactory;
        }

        /// <inheritdoc/>
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(RepeatAfterMeFlow);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Launch Menu flow
        /// </summary>
        private Task RepeatAfterMeFlow(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(AfterRepeatAfterMeFlow);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Catch menu flow end result
        /// </summary>
        private async Task AfterRepeatAfterMeFlow(IDialogContext context, IAwaitable<object> awaitable)
        {
            var result = await awaitable;

            await context.PostAsync($"You said {((Activity)result).Text}");
            await RepeatAfterMeFlow(context, awaitable);
        }

    }
}
