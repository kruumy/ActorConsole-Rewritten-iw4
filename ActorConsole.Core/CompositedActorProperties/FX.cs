namespace ActorConsole.Core.CompositedActorProperties
{
    public class FX : CompositedActorProperty, IKeybindable
    {
        private bool _OnActorBack;
        private bool _OnDeath;
        private Bone _Bone;
        private EffectName _Effect;

        internal FX( Actor Parent ) : base(Parent)
        {
        }

        public bool OnActorBack
        {
            get => _OnActorBack; set
            {
                _OnActorBack = value;
                if ( value )
                {
                    Memory.IW4.Send($"mvm_actor_fx_actorback {Parent.Name} {Bone} {Effect}");
                }
                else
                {
                    Memory.IW4.Send($"mvm_actor_fx_actorback {Parent.Name} ");
                }

                RaisePropertyChanged(nameof(OnActorBack));
            }
        }

        public bool OnDeath
        {
            get => _OnDeath; set
            {
                _OnDeath = value;
                if ( value )
                {
                    Memory.IW4.Send($"mvm_actor_fx_death {Parent.Name} {Bone} {Effect}");
                }
                else
                {
                    Memory.IW4.Send($"mvm_actor_fx_death {Parent.Name} ");
                }
                RaisePropertyChanged(nameof(OnDeath));
            }
        }

        public Bone Bone
        {
            get => _Bone; set
            {
                _Bone = value;
                Memory.IW4.Send($"mvm_actor_fx {Parent.Name} {Bone} {Effect}");
                RaisePropertyChanged(nameof(Bone));
            }
        }

        public EffectName Effect
        {
            get => _Effect; set
            {
                _Effect = value;
                Memory.IW4.Send($"mvm_actor_fx {Parent.Name} {Bone} {Effect}");
                RaisePropertyChanged(nameof(Effect));
            }
        }

        public enum EffectName
        {
            None,
            shot1,
            shot2,
            shotgun,
            headshot,
            blood,
            flash,
            explosion1,
            explosion2,
            explosion3
        }

        public void Play()
        {
            Memory.IW4.Send($"mvm_actor_fx {Parent.Name} {Bone} {Effect}");
        }

        public void Keybind( char key )
        {
            Memory.IW4.Send($"bind {key} \"mvm_actor_fx {Parent.Name} {Bone} {Effect}\"");
        }
    }
}
