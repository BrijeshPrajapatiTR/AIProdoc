# AIProdoc
Repo from AI Test

---

## Converting TPS Files to SQL (no TopSpeed required)

All `.TPS` files live in `TPS/`. The script `convert_tps_to_sql.sh` converts
each one to a `.sql` file under `output/sql/` using **tps2csv** and **sqlite3**.

### Prerequisites

* [`tps2csv`](https://github.com/mtots/tps2csv) installed and on your `PATH`
* `sqlite3` installed (available in most package managers: `apt install sqlite3`,
  `brew install sqlite`, etc.)

---

### Step 1 – Clone the repository

```bash
git clone https://github.com/BrijeshPrajapatiTR/AIProdoc.git
cd AIProdoc
```

### Step 2 – Switch to the TPSBranch

```bash
git checkout TPSBranch
```

### Step 3 – Run the conversion

```bash
bash convert_tps_to_sql.sh
```

The script loops over every `*.TPS` / `*.tps` file in `TPS/`, converts each
one to CSV with `tps2csv`, imports the CSV into a temporary SQLite database,
then dumps a `.sql` file into `output/sql/`.

#### Running the loop directly (inline one-liner)

```bash
mkdir -p output/sql
for f in TPS/*.TPS TPS/*.tps; do
    [ -e "$f" ] || continue
    base=$(basename "$f"); table="${base%.*}"
    tmp=$(mktemp -d)
    tps2csv "$f" > "$tmp/${table}.csv"
    sqlite3 "$tmp/${table}.db" <<SQL
.mode csv
.import '$tmp/${table}.csv' $table
SQL
    sqlite3 "$tmp/${table}.db" ".dump $table" > "output/sql/${table}.sql"
    rm -rf "$tmp"
    echo "Written output/sql/${table}.sql"
done
```

### Step 4 – Commit and push the generated SQL files

```bash
git add output/sql/
git commit -m "Converted TPS files to SQL without topspeed"
git push origin TPSBranch
```
