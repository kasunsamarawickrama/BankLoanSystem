(function(window) {

    'use strict';

    

    // jQuery(document).ready(function() {
    Object.defineProperty(Element.prototype, 'classList', {
      get: function () {
          var self = this, bValue = self.className.split(" ");

          bValue.add = function () {
              var b;
              for (i in arguments) {
                  b = true;
                  for (var j = 0; j < bValue.length; j++)
                      if (bValue[j] === arguments[i]) {
                          b = false;
                          break;
                      }
                  if (b)
                      self.className += (self.className ? " " : "") + arguments[i];
              }
          }
          bValue.remove = function () {
              self.className = "";
              for (i in arguments)
                  for (var j = 0; j < bValue.length; j++)
                      if (bValue[j] !== arguments[i])
                          self.className += (self.className ? " " : "") + bValue[j];
          }
          bValue.toggle = function (x) {
              var b;
              if (x) {
                  self.className = "";
                  b = false;
                  for (var j = 0; j < bValue.length; j++)
                      if (bValue[j] !== x) {
                          self.className += (self.className ? " " : "") + bValue[j];
                          b = false;
                      } else b = true;
                  if (!b)
                      self.className += (self.className ? " " : "") + x;
              } else throw new TypeError("Failed to execute 'toggle': 1 argument required");
              return !b;
          }

          return bValue;
      },
      enumerable: false

  });
  /**
   * Extend Object helper function.
   */
  function extend(a, b) {
    for(var key in b) { 
      if(b.hasOwnProperty(key)) {
        a[key] = b[key];
      }
    }
    return a;
  }

  /**
   * Each helper function.
   */
  function each(collection, callback) {
    for (var i = 0; i < collection.length; i++) {
      var item = collection[i];
      callback(item);
    }
  }

  /**
   * Menu Constructor.
   */
  function menu(options) {
    this.options = extend({}, this.options);
    extend(this.options, options);
    this._init();
  }

  /**
   * Menu Options.
   */
  menu.prototype.options = {
    wrapper: '#o-wrapper',          // The content wrapper
    type: 'slide-left',             // The menu type
    menuOpenerClass: '.c-button',   // The menu opener class names (i.e. the buttons)
    maskId: '#c-mask'               // The ID of the mask
  };

  /**
   * Initialise Menu.
   */
  menu.prototype._init = function() {
    this.body = document.body;
    this.wrapper = document.querySelector(this.options.wrapper);
    this.mask = document.querySelector(this.options.maskId);
    this.menu = document.querySelector('#c-menu--' + this.options.type);
    this.closeBtn = this.menu.querySelector('.c-menu__close');
    this.menuOpeners = document.querySelectorAll(this.options.menuOpenerClass);
    this._initEvents();
  };

  /**
   * Initialise Menu Events.
   */
  menu.prototype._initEvents = function() {
    // Event for clicks on the close button inside the menu.
    this.closeBtn.addEventListener('click', function(e) {
      e.preventDefault();
      this.close();
    }.bind(this));

    // Event for clicks on the mask.
    this.mask.addEventListener('click', function(e) {
      e.preventDefault();
      this.bind();
    }.bind(this));
  };

  /**
   * Open Menu.
   */
  menu.prototype.open = function() {
    this.body.classList.add('has-active-menu');
    this.wrapper.classList.add('has-' + this.options.type);
    this.menu.classList.add('is-active');
    this.mask.classList.add('is-active');
    this.disableMenuOpeners();
  };

  /**
   * Close Menu.
   */
  menu.prototype.close = function() {
    this.body.classList.remove('has-active-menu');
    this.wrapper.classList.remove('has-' + this.options.type);
    this.menu.classList.remove('is-active');
    this.mask.classList.remove('is-active');
    this.enableMenuOpeners();
  };

  /**
   * Disable Menu Openers.
   */
  menu.prototype.disableMenuOpeners = function() {
    each(this.menuOpeners, function(item) {
      item.disabled = true;
    });
  };

  /**
   * Enable Menu Openers.
   */
  menu.prototype.enableMenuOpeners = function() {
    each(this.menuOpeners, function(item) {
      item.disabled = false;
    });
  };

  /**
   * Add to global namespace.
   */
  window.Menu = menu;

})(window);