#!/bin/bash

echo "Starte umfassende XAML-Reparatur..."

# Finde alle XAML-Dateien und repariere sie systematisch
find Features -name "*.axaml" | while read file; do
    echo "Bearbeite: $file"
    
    # Backup erstellen
    cp "$file" "$file.bak"
    
    # 1. Repariere fehlende > nach x:Class
    sed -i '' 's/x:Class="\([^"]*\)"$/x:Class="\1">/g' "$file"
    
    # 2. Stelle sicher, dass nach x:Class eine neue Zeile kommt
    sed -i '' '/x:Class=.*">$/a\
' "$file"
    
    # 3. Entferne doppelte Leerzeilen
    sed -i '' '/^$/N;/^\n$/d' "$file"
    
    echo "  ✓ Repariert: $file"
done

echo "XAML-Reparatur abgeschlossen!"
echo "Teste Build..." 