﻿using ActorConsole.Core.CompositedActorProperties;
using System;
using System.ComponentModel;

namespace ActorConsole.Core
{
    public sealed class Actor : INotifyPropertyChanged, IDisposable
    {
        private uint _Health = 120;
        private string _Name;

        public Actor()
        {
            Count++;
            _Name = $"actor{Count}";
            Anims = new Anims(this);
            Models = new Models(this);
            Weapons = new Weapons(this);
            Walking = new Walking(this);
            FX = new FX(this);
            Manager.Instance.Game.Send($"mvm_actor_spawn {Models.BODY_DEFAULT} {Models.HEAD_DEFAULT}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static uint Count { get; internal set; }
        public Anims Anims { get; private set; }

        public FX FX { get; private set; }

        public uint Health
        {
            get => _Health;
            set
            {
                _Health = value;
                Manager.Instance.Game.Send($"mvm_actor_heath {Name} {value}");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Health)));
            }
        }

        public Models Models { get; private set; }

        public string Name
        {
            get => _Name;
            set
            {
                Manager.Instance.Game.Send($"mvm_actor_rename {Name} {value}");
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public Walking Walking { get; private set; }
        public Weapons Weapons { get; private set; }

        public void Dispose()
        {
            Anims = null;
            Models = null;
            Weapons = null;
            Walking = null;
            FX = null;
            Manager.Instance.Game.Send($"mvm_actor_delete {Name}");
        }

        public void MoveToLocalPlayer()
        {
            Manager.Instance.Game.Send($"mvm_actor_move {Name}");
        }
    }
}
