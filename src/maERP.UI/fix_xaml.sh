#!/bin/bash

# Repariere alle XAML-Dateien mit fehlendem > nach x:Class
echo "Repariere XAML-Dateien..."

# Finde alle XAML-Dateien und repariere sie
find Features -name "*.axaml" | while read file; do
    echo "Repariere: $file"
    
    # Repariere fehlende > nach x:Class
    sed -i '' 's/x:Class="\([^"]*\)"$/x:Class="\1">/g' "$file"
    
    # Entferne leere Zeilen nach x:Class
    sed -i '' '/x:Class=.*">$/N; s/x:Class=\([^>]*\)>\n$/x:Class=\1>\n/g' "$file"
done

echo "XAML-Reparatur abgeschlossen!" 