using System;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Game_Engine.Engine.States;

namespace Game_Engine.Engine.Audio
{
    public class SoundManager
    {
        private int _soundTrackIndex = -1;
        private List<SoundEffectInstance> _soundTracks;
        private Dictionary<Type, SoundEffect> _soundBank = new Dictionary<Type, SoundEffect>();
        public void SetSoundtrack(List<SoundEffectInstance> tracks)
        {
            _soundTracks = tracks;
            _soundTrackIndex = _soundTracks.Count - 1;
        }

        public void RegisterSound(BaseGameStateEvent gameEvent,SoundEffect soundEffect)
        {
            _soundBank.Add(gameEvent.GetType(), soundEffect);
        }
        public void OnNotify(BaseGameStateEvent gameEvent)
        {
            if (_soundBank.ContainsKey(gameEvent.GetType()))
            {
                var sound = _soundBank[gameEvent.GetType()];
                sound.Play();
            }
        }
        public void PlaySoundtrack()
        {
            var nbOfTracks = _soundTracks.Count;

            if (nbOfTracks <= 0)
            {
                return;
            }

            var currentTrack = _soundTracks[_soundTrackIndex];
            var nextTrack = _soundTracks[(_soundTrackIndex + 1) % nbOfTracks];

            if (currentTrack.State == SoundState.Stopped)
            {
                nextTrack.Play();
                _soundTrackIndex++;

                if (_soundTrackIndex >= _soundTracks.Count)
                {
                    _soundTrackIndex = 0;
                }
            }
        }
    }
}
