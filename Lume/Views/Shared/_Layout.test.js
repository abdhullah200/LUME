/**
 * Structural/content tests for Lume/Views/Shared/_Layout.cshtml.
 *
 * The layout is a Razor view, so it is not executed in these tests (no
 * ASP.NET rendering pipeline is set up). Instead these tests check the raw
 * markup for the elements this PR introduced/removed, protecting against
 * accidental regressions such as re-adding the old Bootstrap navbar/footer
 * or losing the hooks (`data-*` attributes, element ids) that
 * wwwroot/js/script.js depends on.
 */

const fs = require('fs');
const path = require('path');

const layoutPath = path.join(__dirname, '_Layout.cshtml');
const layout = fs.readFileSync(layoutPath, 'utf8');

describe('_Layout.cshtml', () => {
  test('references the new style.css stylesheet and drops the old bootstrap/site.css links', () => {
    expect(layout).toContain('~/css/style.css');
    expect(layout).not.toContain('~/css/site.css');
    expect(layout).not.toContain('bootstrap.min.css');
  });

  test('still references tailwind.css and the generated Lume.styles.css', () => {
    expect(layout).toContain('~/css/tailwind.css');
    expect(layout).toContain('~/Lume.styles.css');
  });

  test('renders the site logo with the expected image path', () => {
    expect(layout).toContain('~/images/logo.png');
  });

  test('renders the user avatar with the expected image path in both the trigger and the dropdown', () => {
    const matches = layout.match(/~\/images\/avatar\/user\.png/g) || [];
    expect(matches.length).toBeGreaterThanOrEqual(2);
  });

  test('defines a search box with the id used by the inline placeholder-color style', () => {
    expect(layout).toContain('id="search--box"');
    expect(layout).toContain('#search--box input::placeholder');
  });

  test('defines a notification button and dropdown with the data attributes used by script.js', () => {
    expect(layout).toContain('data-notification-btn');
    expect(layout).toContain('data-notification-dropdown');
  });

  test('defines a profile button and dropdown with the data attributes used by script.js', () => {
    expect(layout).toContain('data-profile-btn');
    expect(layout).toContain('data-profile-dropdown');
  });

  test('no longer contains the old bootstrap navbar markup', () => {
    expect(layout).not.toContain('navbar-expand-sm');
    expect(layout).not.toContain('navbar-toggler');
    expect(layout).not.toContain('asp-controller="Home" asp-action="Privacy"');
  });

  test('no longer renders the old footer', () => {
    expect(layout).not.toContain('<footer');
  });

  test('still renders the body content via RenderBody', () => {
    expect(layout).toContain('@RenderBody()');
  });

  test('includes script.js alongside its sibling asset scripts, after jquery/bootstrap/site.js', () => {
    expect(layout).toContain('~/js/uikit.min.js');
    expect(layout).toContain('~/js/simplebar.js');
    expect(layout).toContain('~/js/script.js');

    const scriptIndex = layout.indexOf('~/js/script.js');
    const siteJsIndex = layout.indexOf('~/js/site.js');
    expect(siteJsIndex).toBeGreaterThan(-1);
    expect(scriptIndex).toBeGreaterThan(siteJsIndex);
  });

  test('still loads the ionicons module scripts used by <ion-icon> elements', () => {
    expect(layout).toContain('ionicons.esm.js');
    expect(layout).toContain('ionicons.js');
  });

  test('has balanced <script> opening/closing tags', () => {
    const opens = (layout.match(/<script\b/g) || []).length;
    const closes = (layout.match(/<\/script>/g) || []).length;
    expect(opens).toBe(closes);
    expect(opens).toBeGreaterThan(0);
  });

  test('has exactly one <head> section', () => {
    expect(layout.match(/<head>/g)).toHaveLength(1);
    expect(layout.match(/<\/head>/g)).toHaveLength(1);
  });

  test('still calls RenderSectionAsync for the optional Scripts section', () => {
    expect(layout).toContain('@await RenderSectionAsync("Scripts", required: false)');
  });
});