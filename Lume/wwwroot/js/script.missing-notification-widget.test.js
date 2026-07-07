/**
 * Regression test documenting the current behaviour of
 * Lume/wwwroot/js/script.js when the profile widget exists in the DOM but
 * the notification dropdown does not.
 *
 * The profile click handler unconditionally references the
 * `notificationDropdown` variable:
 *
 *   profileBtn.addEventListener('click', function (e) {
 *       e.stopPropagation();
 *       profileDropdown.classList.toggle('hidden');
 *       notificationDropdown.classList.add('hidden');
 *   });
 *
 * If `[data-notification-dropdown]` is not present, `notificationDropdown`
 * is `null`, so clicking the profile button raises a TypeError from inside
 * the click handler. This is a known limitation of the current
 * implementation (it is not guarded), and this test exists so any change to
 * that behaviour is intentional and visible in the test suite.
 *
 * Per the DOM spec (and as implemented by jsdom), an exception thrown
 * inside an event listener does NOT propagate synchronously out of
 * `element.click()` / `dispatchEvent()` - it is reported as an uncaught
 * error on `window` instead. These tests capture that error via the
 * `window` "error" event rather than wrapping `.click()` in `toThrow()`.
 *
 * This scenario lives in its own file so it gets a fresh jsdom
 * document/module registry, independent from script.test.js.
 */

const path = require('path');

test('clicking the profile button raises an uncaught error when the notification dropdown element is missing', () => {
  document.body.innerHTML = `
    <button type="button" data-profile-btn>Profile</button>
    <div data-profile-dropdown class="hidden">Profile Menu</div>
  `;

  require(path.join(__dirname, 'script.js'));
  document.dispatchEvent(new Event('DOMContentLoaded'));

  const profileBtn = document.querySelector('[data-profile-btn]');
  const profileDropdown = document.querySelector('[data-profile-dropdown]');

  const uncaughtErrors = [];
  window.addEventListener('error', (event) => uncaughtErrors.push(event.error));

  profileBtn.click();

  expect(uncaughtErrors).toHaveLength(1);
  expect(uncaughtErrors[0]).toBeInstanceOf(TypeError);
  expect(uncaughtErrors[0].message).toMatch(/classList/);

  // The profile dropdown toggle (line before the faulty
  // notificationDropdown reference) still runs before the error is raised.
  expect(profileDropdown.classList.contains('hidden')).toBe(false);
});

test('the DOMContentLoaded handler itself does not throw when the notification widget is missing', () => {
  document.body.innerHTML = `
    <button type="button" data-profile-btn>Profile</button>
    <div data-profile-dropdown class="hidden">Profile Menu</div>
  `;

  expect(() => {
    require(path.join(__dirname, 'script.js'));
    document.dispatchEvent(new Event('DOMContentLoaded'));
  }).not.toThrow();
});

test('clicking outside the widgets does not throw even when the notification dropdown is missing', () => {
  document.body.innerHTML = `
    <button type="button" data-profile-btn>Profile</button>
    <div data-profile-dropdown class="hidden">Profile Menu</div>
  `;

  require(path.join(__dirname, 'script.js'));
  document.dispatchEvent(new Event('DOMContentLoaded'));

  expect(() => document.body.click()).not.toThrow();
});