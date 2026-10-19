<!--
SPDX-FileCopyrightText: 2024-2026 Friedrich von Never <friedrich@fornever.me>

SPDX-License-Identifier: MIT
-->

DevOps [![Status Aquana][status-aquana]][andivionian-status-classifier]
========
Infrastructure as code for Alexylva University.

Configuration
-------------
1. Copy `settings.example.json` to `settings.json`.
2. Set up the values in the `settings.json`.

Usage
-----
How to check if there's anything to apply:
```console
$ dotnet run --project DevOps -- check
```

How to apply the pending changes:
```console
$ dotnet run --project DevOps -- apply
```

Documentation
-------------
- [Contributor Guide][docs.contributing]

License
-------
The project is distributed under the terms of [the MIT license][docs.license].

The license indication in the project's sources is compliant with the [REUSE specification v3.3][reuse.spec].

[andivionian-status-classifier]: https://andivionian.fornever.me/v1/#status-aquana-
[docs.contributing]: CONTRIBUTING.md
[docs.license]: LICENSE.txt
[reuse.spec]: https://reuse.software/spec-3.3/
[status-aquana]: https://img.shields.io/badge/status-aquana-yellowgreen.svg
