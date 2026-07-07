/**
 * Tests for Lume/Lume.csproj.
 *
 * This PR adds an <ItemGroup> that explicitly includes the (otherwise
 * empty, so not auto-discovered) wwwroot/images/avatar folder, so it is
 * tracked and published correctly. Since there is no .NET SDK available to
 * `dotnet build` in this test environment, these tests validate the project
 * file at the XML/text level: well-formedness plus the specific settings
 * and item introduced by this change.
 */

const fs = require('fs');
const path = require('path');

const csprojPath = path.join(__dirname, 'Lume.csproj');
const csproj = fs.readFileSync(csprojPath, 'utf8');

describe('Lume.csproj', () => {
  test('is well-formed XML', () => {
    const parser = new DOMParser();
    const doc = parser.parseFromString(csproj, 'application/xml');
    const errorNode = doc.querySelector('parsererror');
    expect(errorNode).toBeNull();
  });

  test('has a single root <Project> element using the Web SDK', () => {
    expect(csproj).toMatch(/<Project Sdk="Microsoft\.NET\.Sdk\.Web">/);
    expect((csproj.match(/<Project\b/g) || []).length).toBe(1);
  });

  test('targets net8.0 with nullable and implicit usings enabled', () => {
    expect(csproj).toContain('<TargetFramework>net8.0</TargetFramework>');
    expect(csproj).toContain('<Nullable>enable</Nullable>');
    expect(csproj).toContain('<ImplicitUsings>enable</ImplicitUsings>');
  });

  test('includes the wwwroot/images/avatar folder so it is tracked even without files matched by default globs', () => {
    expect(csproj).toMatch(/<Folder\s+Include="wwwroot\\images\\avatar\\"\s*\/>/);
  });

  test('declares the Folder include inside its own <ItemGroup>', () => {
    const itemGroupMatch = csproj.match(/<ItemGroup>([\s\S]*?)<\/ItemGroup>/);
    expect(itemGroupMatch).not.toBeNull();
    expect(itemGroupMatch[1]).toContain('Folder Include="wwwroot\\images\\avatar\\"');
  });

  test('has balanced ItemGroup and PropertyGroup tags', () => {
    const propOpens = (csproj.match(/<PropertyGroup>/g) || []).length;
    const propCloses = (csproj.match(/<\/PropertyGroup>/g) || []).length;
    const itemOpens = (csproj.match(/<ItemGroup>/g) || []).length;
    const itemCloses = (csproj.match(/<\/ItemGroup>/g) || []).length;

    expect(propOpens).toBe(propCloses);
    expect(itemOpens).toBe(itemCloses);
    expect(propOpens).toBeGreaterThan(0);
    expect(itemOpens).toBeGreaterThan(0);
  });
});