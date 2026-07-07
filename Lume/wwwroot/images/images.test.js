/**
 * Sanity tests for the binary image assets added by this PR:
 *   - Lume/wwwroot/images/logo.png
 *   - Lume/wwwroot/images/avatar/user.png
 *
 * These are static assets with no executable behaviour, so the checks here
 * focus on catching accidental corruption/empty commits and validating the
 * files really are recognizable image formats by inspecting their magic
 * bytes, rather than trusting the file extension.
 */

const fs = require('fs');
const path = require('path');

const PNG_SIGNATURE = Buffer.from([0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a]);
const RIFF_SIGNATURE = Buffer.from('RIFF', 'ascii');
const WEBP_TAG = Buffer.from('WEBP', 'ascii');

function readHeader(filePath, length) {
  const fd = fs.openSync(filePath, 'r');
  const buffer = Buffer.alloc(length);
  fs.readSync(fd, buffer, 0, length, 0);
  fs.closeSync(fd);
  return buffer;
}

function isPng(buffer) {
  return buffer.slice(0, 8).equals(PNG_SIGNATURE);
}

function isWebp(buffer) {
  return buffer.slice(0, 4).equals(RIFF_SIGNATURE) && buffer.slice(8, 12).equals(WEBP_TAG);
}

describe.each([
  ['logo.png', path.join(__dirname, 'logo.png')],
  ['avatar/user.png', path.join(__dirname, 'avatar', 'user.png')],
])('%s', (_name, filePath) => {
  test('exists on disk', () => {
    expect(fs.existsSync(filePath)).toBe(true);
  });

  test('is a non-empty file', () => {
    const stats = fs.statSync(filePath);
    expect(stats.size).toBeGreaterThan(0);
  });

  test('is a recognizable image format (PNG or WebP)', () => {
    const header = readHeader(filePath, 12);
    expect(isPng(header) || isWebp(header)).toBe(true);
  });
});

describe('logo.png', () => {
  test('has a genuine PNG file signature', () => {
    const header = readHeader(path.join(__dirname, 'logo.png'), 8);
    expect(isPng(header)).toBe(true);
  });
});

describe('avatar/user.png', () => {
  // Regression note: despite the ".png" extension, the committed file's
  // bytes are a RIFF/WEBP container, not a real PNG. This test documents
  // the actual current content of the file rather than asserting an
  // incorrect PNG signature. If the asset is ever replaced with a genuine
  // PNG, this test should be updated accordingly.
  test('is actually encoded as WebP (RIFF/WEBP), not a true PNG, despite its extension', () => {
    const header = readHeader(path.join(__dirname, 'avatar', 'user.png'), 12);
    expect(isWebp(header)).toBe(true);
    expect(isPng(header)).toBe(false);
  });
});