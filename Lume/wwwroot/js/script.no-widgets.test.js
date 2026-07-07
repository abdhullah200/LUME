/**
 * Regression test for Lume/wwwroot/js/script.js when neither the
 * notification widget nor the profile widget are present in the DOM (e.g. a
 * page that does not use the shared layout header). The setup code guards
 * both `if (notificationBtn && notificationDropdown)` and
 * `if (profileBtn && profileDropdown)` before attaching click listeners, so
 * nothing should throw in this scenario, including the outside-click
 * handler which itself guards on both dropdown variables.
 */

const path = require('path');

test('does not throw on DOMContentLoaded when neither widget exists', () => {
  document.body.innerHTML = '<div id="content">No widgets here</div>';

  expect(() => {
    require(path.join(__dirname, 'script.js'));
    document.dispatchEvent(new Event('DOMContentLoaded'));
  }).not.toThrow();
});

test('does not throw when clicking anywhere in the document', () => {
  document.body.innerHTML = '<div id="content">No widgets here</div>';

  require(path.join(__dirname, 'script.js'));
  document.dispatchEvent(new Event('DOMContentLoaded'));

  expect(() => document.body.click()).not.toThrow();
});