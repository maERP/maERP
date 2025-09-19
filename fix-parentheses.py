#!/usr/bin/env python3
import os
import re
import glob

def fix_parentheses_in_file(file_path):
    """Fix missing closing parentheses in C# test files."""
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            content = f.read()
        
        original_content = content
        
        # Fix patterns like: .Where(p => p.TenantId == TenantConstants.TestTenant1Id
        # Should become: .Where(p => p.TenantId == TenantConstants.TestTenant1Id)
        
        patterns = [
            # Where clauses
            (r'\.Where\([^)]+TenantId == TenantConstants\.TestTenant[12]Id$', lambda m: m.group(0) + ')'),
            # CountAsync
            (r'\.CountAsync\([^)]+TenantId == TenantConstants\.TestTenant[12]Id$', lambda m: m.group(0) + ')'),
            # AnyAsync
            (r'\.AnyAsync\([^)]+TenantId == TenantConstants\.TestTenant[12]Id$', lambda m: m.group(0) + ')'),
            # FirstOrDefaultAsync
            (r'\.FirstOrDefaultAsync\([^)]+TenantId == TenantConstants\.TestTenant[12]Id$', lambda m: m.group(0) + ')'),
        ]
        
        # Apply multiline fixes
        lines = content.split('\n')
        for i, line in enumerate(lines):
            # Check for lines that end with TenantConstants.TestTenant[12]Id and are part of LINQ queries
            if re.search(r'TenantId == TenantConstants\.TestTenant[12]Id$', line.strip()):
                # Check if this line is missing a closing parenthesis
                if '(' in line and line.count('(') > line.count(')'):
                    lines[i] = line + ')'
        
        content = '\n'.join(lines)
        
        # Only write if content changed
        if content != original_content:
            with open(file_path, 'w', encoding='utf-8') as f:
                f.write(content)
            print(f"Fixed: {file_path}")
            return True
            
    except Exception as e:
        print(f"Error processing {file_path}: {e}")
    
    return False

def main():
    """Main function to fix all C# test files."""
    print("Fixing parentheses in test files...")
    
    # Find all C# files in test directories
    test_patterns = [
        'tests/**/*.cs',
        'tests/**/**/*.cs',
        'tests/**/**/**/*.cs'
    ]
    
    files_fixed = 0
    for pattern in test_patterns:
        for file_path in glob.glob(pattern, recursive=True):
            if '/obj/' not in file_path and '/bin/' not in file_path:
                if fix_parentheses_in_file(file_path):
                    files_fixed += 1
    
    print(f"Fixed {files_fixed} files.")

if __name__ == "__main__":
    main()