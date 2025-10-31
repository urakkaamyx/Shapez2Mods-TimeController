
# 🕒 Time Controller (Shapez 2 Mod)

**Author:** Urakka  
**Game:** Shapez 2  
**Framework:** BepInEx  
**Version:** 1.0.0  

## 🎮 Features

- ⏸️ **Pause** the game instantly  
- ▶️ **Resume** at normal speed (1x)  
- ⏩ **Fast-forward** up to 5x, 10x, or 25x  
- 💾 Works automatically when starting a new or loaded save  

---

## ⚙️ Speed Levels

| Button | Speed | Description |
|--------|--------|-------------|
| II | 0x | Pause |
| > | 1x | Normal |
| >> | 5x | Fast |
| >>> | 10x | Very Fast |
| >\| | 25x | Ultra Speed |

## 🧠 Technical Details

- Namespace: `Shapez2Mods.TimeController`
- Hooks into: `GameCore.Init()` via Harmony patch
- Utilizes: `SimulationSpeedManager` for speed control