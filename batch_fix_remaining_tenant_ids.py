#!/usr/bin/env python3

import os
import re
from pathlib import Path

def fix_remaining_tenant_ids_in_file(file_path):
    """Fix remaining TenantId issues in a single C# test file"""
    print(f"Processing: {file_path}")
    
    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    original_content = content
    
    # Add using statement for TenantConstants if patterns need it and not already present
    needs_constants = any(pattern in content for pattern in [
        'new[] { 1, 2, 3 }',
        'new[] { 1, 2, 3, 4, 5 }',
        'TenantConstants.'
    ])
    
    if needs_constants and 'using maERP.Domain.Constants;' not in content:
        # Find the last using statement and insert after it
        using_pattern = r'((?:using [^;]+;\n)*)'
        match = re.search(using_pattern, content)
        if match:
            existing_usings = match.group(1)
            if 'using maERP.Domain.Constants;' not in existing_usings:
                new_usings = existing_usings + 'using maERP.Domain.Constants;\n'
                content = content.replace(existing_usings, new_usings)
    
    # Replace remaining SetAssignedTenantIds patterns
    content = re.sub(
        r'SetAssignedTenantIds\(new\[\] \{ 1, 2, 3 \}\)',
        'SetAssignedTenantIds(new[] { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id, Guid.NewGuid() })',
        content
    )
    
    content = re.sub(
        r'SetAssignedTenantIds\(new\[\] \{ 1, 2, 3, 4, 5 \}\)',
        'SetAssignedTenantIds(new[] { TenantConstants.TestTenant1Id, TenantConstants.TestTenant2Id, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() })',
        content
    )
    
    # Only write if content changed
    if content != original_content:
        with open(file_path, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"  Updated: {file_path}")
        return True
    else:
        print(f"  No changes: {file_path}")
        return False

def main():
    test_dir = Path('/Users/martin/Projekte/martin-andrich/maERP/tests/maERP.Server.Tests')
    
    # Find all C# files with the remaining problematic patterns
    files_to_fix = []
    
    for cs_file in test_dir.rglob('*.cs'):
        with open(cs_file, 'r', encoding='utf-8') as f:
            content = f.read()
            if any(pattern in content for pattern in [
                'SetAssignedTenantIds(new[] { 1, 2, 3 })',
                'SetAssignedTenantIds(new[] { 1, 2, 3, 4, 5 })'
            ]):
                files_to_fix.append(cs_file)
    
    print(f"Found {len(files_to_fix)} files with remaining patterns to fix")
    
    fixed_count = 0
    for file_path in files_to_fix:
        if fix_remaining_tenant_ids_in_file(file_path):
            fixed_count += 1
    
    print(f"Fixed {fixed_count} files")

if __name__ == '__main__':
    main()