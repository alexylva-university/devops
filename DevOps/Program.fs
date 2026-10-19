// SPDX-FileCopyrightText: 2026 Friedrich von Never <friedrich@fornever.me>
//
// SPDX-License-Identifier: MIT

open System.Collections.Generic
open System.Text.Json
open Fabricator.Core
open Fabricator.Resources
open Fabricator.Secrets.KeePass
open TruePath
open TruePath.SystemIo

let private configFile = AbsolutePath(__SOURCE_DIRECTORY__) / ".." / "settings.json"
let private config =
    if not <| configFile.ExistsFile() then failwith $"Missing file \"{configFile.Value}\"."
    configFile.OpenRead()
    |> JsonSerializer.Deserialize<Dictionary<string, string>>
    |> nonNull

let private secretStore =
    let path = AbsolutePath config["kdbx.path"]
    KeePassSecretStore(path)

let private resources = [
    DigitalOceanDns.Domain(
        "alexylva.university",
        { Store = secretStore; Name = SecretName config["kdbx.token-name"] },
        [
            DigitalOceanDns.Record(
                "A",
                "@",
                config["ip"],
                ttl = 3600
            )
            DigitalOceanDns.Record(
                "TXT",
                "@",
                "protonmail-verification=c3a354a26e36bab3b9da330e6b552382928b9773",
                ttl = 300
            )
        ]
    )
]

[<EntryPoint>]
let main(args: string[]): int =
    Fabricator.Console.EntryPoint.main args resources
