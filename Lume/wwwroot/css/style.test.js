/**
 * Content/consistency tests for Lume/wwwroot/css/style.css.
 *
 * style.css previously contained no rules (just a byte-order mark). This PR
 * fills it in with utility, UIkit component and Simplebar styles. Since CSS
 * has no executable behaviour to unit test in the traditional sense, these
 * tests check structural integrity (balanced braces) and the presence of
 * the specific selectors/keyframes this PR adds, so an accidental partial
 * paste or truncation is caught.
 */

const fs = require('fs');
const path = require('path');

const cssPath = path.join(__dirname, 'style.css');
const css = fs.readFileSync(cssPath, 'utf8');

describe('style.css', () => {
  test('is not empty', () => {
    expect(css.trim().length).toBeGreaterThan(0);
  });

  test('has a balanced number of curly braces', () => {
    const opens = (css.match(/\{/g) || []).length;
    const closes = (css.match(/\}/g) || []).length;
    expect(opens).toBeGreaterThan(0);
    expect(opens).toBe(closes);
  });

  test('defines the [hidden] utility rule used to toggle the notification/profile dropdowns', () => {
    expect(css).toMatch(/\[hidden\]\s*\{\s*display:\s*none\s*!important;/);
  });

  test('defines the on/off switch button styles', () => {
    expect(css).toContain('.switch-button');
    expect(css).toContain('.switch input');
    expect(css).toContain('input:checked + .switch-button:before');
  });

  test('defines the ripple effect styles and keyframes', () => {
    expect(css).toContain('.ripple-effect');
    expect(css).toContain('.ripple-effect-dark');
    expect(css).toMatch(/@keyframes ripple/);
  });

  test('defines the uikit modal, slider and lightbox component styles', () => {
    expect(css).toContain('.uk-modal');
    expect(css).toContain('.uk-slider');
    expect(css).toContain('.uk-lightbox');
    expect(css).toContain('.uk-lightbox-items');
  });

  test('defines uikit transform-origin utility classes', () => {
    expect(css).toContain('.uk-transform-origin-top-left');
    expect(css).toContain('.uk-transform-origin-bottom-right');
  });

  test('defines uikit animation keyframes', () => {
    expect(css).toContain('@keyframes uk-fade');
    expect(css).toContain('@keyframes uk-slide-top');
    expect(css).toContain('@keyframes uk-shake');
    expect(css).toContain('@keyframes uk-kenburns');
  });

  test('defines uikit transition helper classes', () => {
    expect(css).toContain('.uk-transition-fade');
    expect(css).toContain('.uk-transition-slide-top');
  });

  test('defines simplebar scrollbar styles', () => {
    expect(css).toContain('[data-simplebar]');
    expect(css).toContain('.simplebar-track');
    expect(css).toContain('.simplebar-scrollbar');
    expect(css).toContain('.simplebar-content');
  });

  test('every animation-name referenced in a rule has a matching @keyframes definition', () => {
    // CSS-wide/animation keyword values (e.g. "none" used to cancel an
    // animation) are not keyframes names and must be excluded.
    const CSS_KEYWORDS = new Set(['none', 'inherit', 'initial', 'unset', 'revert']);

    const referenced = Array.from(css.matchAll(/animation-name:\s*([^;]+);/g))
      .flatMap((m) => m[1].split(',').map((s) => s.trim()))
      .filter((name) => !CSS_KEYWORDS.has(name));
    const defined = Array.from(css.matchAll(/@keyframes\s+([\w-]+)/g)).map((m) => m[1]);

    expect(referenced.length).toBeGreaterThan(0);
    referenced.forEach((name) => {
      expect(defined).toContain(name);
    });
  });
});