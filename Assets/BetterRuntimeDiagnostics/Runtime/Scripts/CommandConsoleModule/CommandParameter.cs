﻿using System;
using System.ComponentModel;
using Better.Extensions.Runtime;
using UnityEngine;

namespace Better.Diagnostics.Runtime.CommandConsoleModule
{
    internal class CommandParameter
    {
        private readonly Type _type;
        private readonly object _value;

        internal Type Type => _type;
        internal object Value => _value;

        internal bool IsValid { get; }
        internal bool IsHelpCommand { get; }

        internal CommandParameter(string command)
        {
            if (command.Contains(CommandDefinition.TypeSplitCommand))
            {
                var split = command.Split(CommandDefinition.TypeSplitCommand, StringSplitOptions.RemoveEmptyEntries);
                var typeString = split[0];
                var valueString = split[1];

                if (CommandDefinition.TryGetType(typeString, out _type))
                {
                    var converter = TypeDescriptor.GetConverter(_type);
                    if (converter.IsValid(valueString))
                    {
                        _value = converter.ConvertFromInvariantString(valueString);
                        IsValid = true;
                    }
                    else
                    {
                        Debug.LogError($"\"{valueString}\" cannot be converted to \"{typeString}\"");
                    }
                }
                else
                {
                    _type = typeof(string);
                    _value = valueString;
                    IsValid = true;
                }
            }
            else
            {
                IsHelpCommand = command.FastEquals(CommandDefinition.HelpCommand);
                if (!IsHelpCommand)
                {
                    if (!CommandDefinition.TryParse(command, out _value, out _type))
                    {
                        _value = command;
                        _type = typeof(string);
                    }
                }

                IsValid = true;
            }
        }
    }
}