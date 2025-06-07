#!/usr/bin/env python3

import os
import re
import glob

def fix_xaml_file(filepath):
    """Repariert eine XAML-Datei"""
    print(f"Repariere: {filepath}")
    
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Backup erstellen
    with open(filepath + '.bak', 'w', encoding='utf-8') as f:
        f.write(content)
    
    # 1. Repariere fehlende > nach x:Class
    content = re.sub(r'x:Class="([^"]*)"$', r'x:Class="\1">', content, flags=re.MULTILINE)
    
    # 2. Stelle sicher, dass nach x:Class eine neue Zeile kommt
    content = re.sub(r'(x:Class="[^"]*">)\s*\n\s*(<)', r'\1\n\n  \2', content)
    
    # 3. Entferne überschüssige Leerzeilen
    content = re.sub(r'\n\n\n+', '\n\n', content)
    
    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(content)
    
    print(f"  ✓ Repariert: {filepath}")

def main():
    print("Starte Python XAML-Reparatur...")
    
    # Finde alle XAML-Dateien
    xaml_files = glob.glob("Features/**/*.axaml", recursive=True)
    
    for xaml_file in xaml_files:
        fix_xaml_file(xaml_file)
    
    print(f"XAML-Reparatur abgeschlossen! {len(xaml_files)} Dateien bearbeitet.")

if __name__ == "__main__":
    main() 