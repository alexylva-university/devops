// SPDX-FileCopyrightText: 2026 Friedrich von Never <friedrich@fornever.me>
//
// SPDX-License-Identifier: MIT

let private resources = []

[<EntryPoint>]
let main(args: string[]): int =
    Fabricator.Console.EntryPoint.main args resources
