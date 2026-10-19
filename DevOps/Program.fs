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
                ttl = 3600
            )
            DigitalOceanDns.Record(
                "MX",
                "@",
                "mail.protonmail.ch.",
                priority = 10,
                ttl = 14400
            )
            DigitalOceanDns.Record(
                "MX",
                "@",
                "mailsec.protonmail.ch.",
                priority = 20,
                ttl = 14400
            )
            DigitalOceanDns.Record(
                "TXT",
                "@",
                "v=spf1 include:_spf.protonmail.ch ~all",
                ttl = 3600
            )
            DigitalOceanDns.Record(
                "CNAME",
                "protonmail._domainkey",
                "protonmail.domainkey.dgzvuasz7nueshjrdb552yis6dmvpzvtnsean6nrs2gs3yxqc4sza.domains.proton.ch.",
                ttl = 43200
            )
            DigitalOceanDns.Record(
                "CNAME",
                "protonmail2._domainkey",
                "protonmail2.domainkey.dgzvuasz7nueshjrdb552yis6dmvpzvtnsean6nrs2gs3yxqc4sza.domains.proton.ch.",
                ttl = 43200
            )
            DigitalOceanDns.Record(
                "CNAME",
                "protonmail3._domainkey",
                "protonmail3.domainkey.dgzvuasz7nueshjrdb552yis6dmvpzvtnsean6nrs2gs3yxqc4sza.domains.proton.ch.",
                ttl = 43200
            )
            DigitalOceanDns.Record(
                "TXT",
                "_dmarc",
                "v=DMARC1; p=quarantine",
                ttl = 3600
            )
        ]
    )
]

[<EntryPoint>]
let main(args: string[]): int =
    Fabricator.Console.EntryPoint.main args resources
