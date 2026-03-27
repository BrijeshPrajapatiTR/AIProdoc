#!/usr/bin/env bash
# convert_tps_to_sql.sh
# Convert every .TPS file in TPS/ to a .sql file in output/sql/
# using tps2csv -> sqlite3 (no Python, no TopSpeed required)

set -euo pipefail

TPS_DIR="TPS"
OUT_DIR="output/sql"
TMPDIR_WORK="$(mktemp -d)"

cleanup() {
    rm -rf "$TMPDIR_WORK"
}
trap cleanup EXIT

mkdir -p "$OUT_DIR"

for tps_file in "$TPS_DIR"/*.TPS "$TPS_DIR"/*.tps; do
    # Skip glob literals that did not match any file
    [ -e "$tps_file" ] || continue

    base="$(basename "$tps_file")"
    table="${base%.*}"
    csv_file="$TMPDIR_WORK/${table}.csv"
    db_file="$TMPDIR_WORK/${table}.db"
    sql_file="$OUT_DIR/${table}.sql"

    echo "Converting $tps_file -> $sql_file"

    # Step 1: convert TPS to CSV
    tps2csv "$tps_file" > "$csv_file"

    # Step 2: import CSV (with header row) into SQLite
    sqlite3 "$db_file" <<SQL
.mode csv
.import '$csv_file' $table
SQL

    # Step 3: dump the table as SQL
    sqlite3 "$db_file" ".dump $table" > "$sql_file"

    echo "  -> $sql_file written."
done

echo "Done. SQL files are in $OUT_DIR/"
