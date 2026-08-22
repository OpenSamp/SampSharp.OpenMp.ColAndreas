# SampSharp.OpenMp.ColAndreas

Managed C# bindings for [ColAndreas](https://github.com/Pottus/ColAndreas) on open.mp x64,
for gamemodes running on the SampSharp open.mp host. ColAndreas keeps a Bullet-physics copy
of the GTA: San Andreas static collision world server-side, so the server can answer
questions the game engine normally only answers on the client — what is under this point,
what does this ray hit, where is the ground here.

Unlike the sibling bridges in this family, this repository is **managed only**. There is no
native shim to build: the bindings P/Invoke the `CASharp_*` exports of `colandreas.dll`
directly.

## Architecture

```
┌──────────────────────────────────────────────────────────────────────┐
│  C# gamemode                                                         │
│     IColAndreasService                                               │
└──────────────────────────────────────────────────────────────────────┘
                               │   P/Invoke (CASharp_* exports)
                               ▼
┌──────────────────────────────────────────────────────────────────────┐
│  colandreas.dll  (open.mp component, Bullet physics + .cadb)         │
└──────────────────────────────────────────────────────────────────────┘
```

## Runtime dependencies

| Component            | Where from                             |
|----------------------|----------------------------------------|
| `colandreas.dll`/`.so` | ColAndreas open.mp build              |
| `SampSharp.dll`      | `SampSharp/src/sampsharp-component/`   |
| .NET 10 runtime      | System-wide                            |
| `ColAndreas.cadb`    | Collision database, ships with the plugin |

`IsDataLoaded` reports whether the `.cadb` was found and parsed; `IsInitialized` reports
whether `Init()` has built the dynamics world. Call `Init()` once at startup — everything
else returns garbage until you do.

## Wiring

```csharp
services.AddColAndreas();
```

## Surface

- **Ray casts** — `RayCastLine` and variants returning the hit point, the collision model
  id, the surface normal, the reflection vector, or the hit object's rotation
- **Ground height** — `FindZ_For2DCoord` / `FindZ_For3DCoord`, casting from `+700` down to
  `-1000` by default: high enough to clear the LV and SF airports, low enough to catch
  underwater terrain
- **Collision objects** — create, destroy, move, rotate and tag user objects in the
  physics world, independent of the visible objects players see
- **Building removal** — `RemoveBuilding` / `RestoreBuilding`, to keep the collision world
  in step with `RemoveBuildingForPlayer` on the client
- **Model geometry** — bounding sphere and bounding box per model id, plus
  `ContactTest` for "would this model at this position intersect anything"
- **Math helpers** — `EulerToQuat` / `QuatToEuler`

Method names deliberately follow the legacy x86 `SampSharp.ColAndreas` binding, including
the unidiomatic `FindZ_For2DCoord`, so gamemode code can move to open.mp x64 without a
rename pass.

## Building

```bash
dotnet build SampSharp.OpenMp.ColAndreas.csproj
```

Needs the SampSharp repository checked out alongside this one; the csproj references
`SampSharp.OpenMp.Core` and `SampSharp.OpenMp.Entities` by relative path.

CI builds on Linux, macOS and Windows and publishes the assembly plus its XML docs as a
build artifact; tagged pushes cut a GitHub release.

## License

Apache-2.0 -- see [LICENSE](LICENSE). That covers the bindings in this
repository; ColAndreas itself is licensed separately by its authors.

---

Powered by [vs-rp.org](https://vs-rp.org)
