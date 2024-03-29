/*!
 * vue-treeselect v0.0.38 | (c) 2017-2019 Riophae Lee
 * Released under the MIT License.
 * https://vue-treeselect.js.org/
 */
!(function (e, t) {
    "object" == typeof exports && "object" == typeof module
        ? (module.exports = t(require("Vue")))
        : "function" == typeof define && define.amd
            ? define(["Vue"], t)
            : "object" == typeof exports
                ? (exports.VueTreeselect = t(require("Vue")))
                : (e.VueTreeselect = t(e.Vue));
})(window, function (n) {
    return (function (n) {
        var i = {};
        function r(e) {
            if (i[e]) return i[e].exports;
            var t = (i[e] = { i: e, l: !1, exports: {} });
            return n[e].call(t.exports, t, t.exports, r), (t.l = !0), t.exports;
        }
        return (
            (r.m = n),
            (r.c = i),
            (r.d = function (e, t, n) {
                r.o(e, t) || Object.defineProperty(e, t, { enumerable: !0, get: n });
            }),
            (r.r = function (e) {
                "undefined" != typeof Symbol &&
                    Symbol.toStringTag &&
                    Object.defineProperty(e, Symbol.toStringTag, { value: "Module" }),
                    Object.defineProperty(e, "__esModule", { value: !0 });
            }),
            (r.t = function (t, e) {
                if ((1 & e && (t = r(t)), 8 & e)) return t;
                if (4 & e && "object" == typeof t && t && t.__esModule) return t;
                var n = Object.create(null);
                if (
                    (r.r(n),
                        Object.defineProperty(n, "default", { enumerable: !0, value: t }),
                        2 & e && "string" != typeof t)
                )
                    for (var i in t)
                        r.d(
                            n,
                            i,
                            function (e) {
                                return t[e];
                            }.bind(null, i)
                        );
                return n;
            }),
            (r.n = function (e) {
                var t =
                    e && e.__esModule
                        ? function () {
                            return e.default;
                        }
                        : function () {
                            return e;
                        };
                return r.d(t, "a", t), t;
            }),
            (r.o = function (e, t) {
                return Object.prototype.hasOwnProperty.call(e, t);
            }),
            (r.p = "/"),
            r((r.s = 37))
        );
    })([
        function (e, t) {
            e.exports = function (e, t, n) {
                return (
                    t in e
                        ? Object.defineProperty(e, t, {
                            value: n,
                            enumerable: !0,
                            configurable: !0,
                            writable: !0
                        })
                        : (e[t] = n),
                    e
                );
            };
        },
        function (e, t, n) {
            var r = n(0);
            e.exports = function (t) {
                for (var e = 1; e < arguments.length; e++) {
                    var n = null != arguments[e] ? arguments[e] : {},
                        i = Object.keys(n);
                    "function" == typeof Object.getOwnPropertySymbols &&
                        (i = i.concat(
                            Object.getOwnPropertySymbols(n).filter(function (e) {
                                return Object.getOwnPropertyDescriptor(n, e).enumerable;
                            })
                        )),
                        i.forEach(function (e) {
                            r(t, e, n[e]);
                        });
                }
                return t;
            };
        },
        function (e, t) {
            var a = /^(attrs|props|on|nativeOn|class|style|hook)$/;
            function l(e, t) {
                return function () {
                    e && e.apply(this, arguments), t && t.apply(this, arguments);
                };
            }
            e.exports = function (e) {
                return e.reduce(function (e, t) {
                    var n, i, r, o, s;
                    for (r in t)
                        if (((n = e[r]), (i = t[r]), n && a.test(r)))
                            if (
                                ("class" === r &&
                                    ("string" == typeof n &&
                                        ((s = n), (e[r] = n = {}), (n[s] = !0)),
                                        "string" == typeof i &&
                                        ((s = i), (t[r] = i = {}), (i[s] = !0))),
                                    "on" === r || "nativeOn" === r || "hook" === r)
                            )
                                for (o in i) n[o] = l(n[o], i[o]);
                            else if (Array.isArray(n)) e[r] = n.concat(i);
                            else if (Array.isArray(i)) e[r] = [n].concat(i);
                            else for (o in i) n[o] = i[o];
                        else e[r] = t[r];
                    return e;
                }, {});
            };
        },
        function (e, t, n) {
            var i = n(22),
                r = n(23),
                o = n(24);
            e.exports = function (e) {
                return i(e) || r(e) || o();
            };
        },
        function (e, t) {
            e.exports = function () { };
        },
        function (e, t, n) {
            var y = n(6),
                S = n(25),
                b = n(8),
                O = Math.max,
                _ = Math.min;
            e.exports = function (i, r, e) {
                var o,
                    s,
                    a,
                    l,
                    u,
                    c,
                    d = 0,
                    h = !1,
                    f = !1,
                    t = !0;
                if ("function" != typeof i) throw new TypeError("Expected a function");
                function p(e) {
                    var t = o,
                        n = s;
                    return (o = s = void 0), (d = e), (l = i.apply(n, t));
                }
                function v(e) {
                    var t = e - c;
                    return void 0 === c || r <= t || t < 0 || (f && a <= e - d);
                }
                function m() {
                    var e,
                        t,
                        n = S();
                    if (v(n)) return g(n);
                    u = setTimeout(
                        m,
                        ((t = r - ((e = n) - c)), f ? _(t, a - (e - d)) : t)
                    );
                }
                function g(e) {
                    return (u = void 0), t && o ? p(e) : ((o = s = void 0), l);
                }
                function n() {
                    var e,
                        t = S(),
                        n = v(t);
                    if (((o = arguments), (s = this), (c = t), n)) {
                        if (void 0 === u)
                            return (d = e = c), (u = setTimeout(m, r)), h ? p(e) : l;
                        if (f) return (u = setTimeout(m, r)), p(c);
                    }
                    return void 0 === u && (u = setTimeout(m, r)), l;
                }
                return (
                    (r = b(r) || 0),
                    y(e) &&
                    ((h = !!e.leading),
                        (a = (f = "maxWait" in e) ? O(b(e.maxWait) || 0, r) : a),
                        (t = "trailing" in e ? !!e.trailing : t)),
                    (n.cancel = function () {
                        void 0 !== u && clearTimeout(u), (o = c = s = u = void (d = 0));
                    }),
                    (n.flush = function () {
                        return void 0 === u ? l : g(S());
                    }),
                    n
                );
            };
        },
        function (e, t) {
            e.exports = function (e) {
                var t = typeof e;
                return null != e && ("object" == t || "function" == t);
            };
        },
        function (e, t, n) {
            var i = n(26),
                r = "object" == typeof self && self && self.Object === Object && self,
                o = i || r || Function("return this")();
            e.exports = o;
        },
        function (e, t, n) {
            var i = n(6),
                r = n(28),
                o = /^\s+|\s+$/g,
                s = /^[-+]0x[0-9a-f]+$/i,
                a = /^0b[01]+$/i,
                l = /^0o[0-7]+$/i,
                u = parseInt;
            e.exports = function (e) {
                if ("number" == typeof e) return e;
                if (r(e)) return NaN;
                if (i(e)) {
                    var t = "function" == typeof e.valueOf ? e.valueOf() : e;
                    e = i(t) ? t + "" : t;
                }
                if ("string" != typeof e) return 0 === e ? e : +e;
                e = e.replace(o, "");
                var n = a.test(e);
                return n || l.test(e) ? u(e.slice(2), n ? 2 : 8) : s.test(e) ? NaN : +e;
            };
        },
        function (e, t, n) {
            var i = n(7).Symbol;
            e.exports = i;
        },
        function (e, t) {
            e.exports = function (e) {
                return (
                    !!e &&
                    ("object" == typeof e || "function" == typeof e) &&
                    "function" == typeof e.then
                );
            };
        },
        function (e, t, n) {
            var i = n(33);
            e.exports = function (e) {
                return i(2, e);
            };
        },
        function (e, t) {
            e.exports = function (e) {
                return e;
            };
        },
        function (e, t) {
            e.exports = function (e) {
                return function () {
                    return e;
                };
            };
        },
        function (e, t) {
            e.exports = function (e) {
                var t = null == e ? 0 : e.length;
                return t ? e[t - 1] : void 0;
            };
        },
        function (e, t, n) {
            var i = n(19),
                r = n(20),
                o = n(21);
            e.exports = function (e, t) {
                return i(e) || r(e, t) || o();
            };
        },
        function (e, t, n) {
            "use strict";
            e.exports = function (e, t) {
                var n = t.length,
                    i = e.length;
                if (n < i) return !1;
                if (i === n) return e === t;
                e: for (var r = 0, o = 0; r < i; r++) {
                    for (var s = e.charCodeAt(r); o < n;)
                        if (t.charCodeAt(o++) === s) continue e;
                    return !1;
                }
                return !0;
            };
        },
        function (t, e) {
            function n(e) {
                return (n =
                    "function" == typeof Symbol && "symbol" == typeof Symbol.iterator
                        ? function (e) {
                            return typeof e;
                        }
                        : function (e) {
                            return e &&
                                "function" == typeof Symbol &&
                                e.constructor === Symbol &&
                                e !== Symbol.prototype
                                ? "symbol"
                                : typeof e;
                        })(e);
            }
            function i(e) {
                return (
                    "function" == typeof Symbol && "symbol" === n(Symbol.iterator)
                        ? (t.exports = i = function (e) {
                            return n(e);
                        })
                        : (t.exports = i = function (e) {
                            return e &&
                                "function" == typeof Symbol &&
                                e.constructor === Symbol &&
                                e !== Symbol.prototype
                                ? "symbol"
                                : n(e);
                        }),
                    i(e)
                );
            }
            t.exports = i;
        },
        function (e, t) {
            e.exports = n;
        },
        function (e, t) {
            e.exports = function (e) {
                if (Array.isArray(e)) return e;
            };
        },
        function (e, t) {
            e.exports = function (e, t) {
                var n = [],
                    i = !0,
                    r = !1,
                    o = void 0;
                try {
                    for (
                        var s, a = e[Symbol.iterator]();
                        !(i = (s = a.next()).done) &&
                        (n.push(s.value), !t || n.length !== t);
                        i = !0
                    );
                } catch (e) {
                    (r = !0), (o = e);
                } finally {
                    try {
                        i || null == a.return || a.return();
                    } finally {
                        if (r) throw o;
                    }
                }
                return n;
            };
        },
        function (e, t) {
            e.exports = function () {
                throw new TypeError(
                    "Invalid attempt to destructure non-iterable instance"
                );
            };
        },
        function (e, t) {
            e.exports = function (e) {
                if (Array.isArray(e)) {
                    for (var t = 0, n = new Array(e.length); t < e.length; t++)
                        n[t] = e[t];
                    return n;
                }
            };
        },
        function (e, t) {
            e.exports = function (e) {
                if (
                    Symbol.iterator in Object(e) ||
                    "[object Arguments]" === Object.prototype.toString.call(e)
                )
                    return Array.from(e);
            };
        },
        function (e, t) {
            e.exports = function () {
                throw new TypeError("Invalid attempt to spread non-iterable instance");
            };
        },
        function (e, t, n) {
            var i = n(7);
            e.exports = function () {
                return i.Date.now();
            };
        },
        function (n, e, t) {
            (function (e) {
                var t = "object" == typeof e && e && e.Object === Object && e;
                n.exports = t;
            }.call(this, t(27)));
        },
        function (e, t) {
            var n;
            n = (function () {
                return this;
            })();
            try {
                n = n || new Function("return this")();
            } catch (e) {
                "object" == typeof window && (n = window);
            }
            e.exports = n;
        },
        function (e, t, n) {
            var i = n(29),
                r = n(32);
            e.exports = function (e) {
                return "symbol" == typeof e || (r(e) && "[object Symbol]" == i(e));
            };
        },
        function (e, t, n) {
            var i = n(9),
                r = n(30),
                o = n(31),
                s = i ? i.toStringTag : void 0;
            e.exports = function (e) {
                return null == e
                    ? void 0 === e
                        ? "[object Undefined]"
                        : "[object Null]"
                    : s && s in Object(e)
                        ? r(e)
                        : o(e);
            };
        },
        function (e, t, n) {
            var i = n(9),
                r = Object.prototype,
                o = r.hasOwnProperty,
                s = r.toString,
                a = i ? i.toStringTag : void 0;
            e.exports = function (e) {
                var t = o.call(e, a),
                    n = e[a];
                try {
                    var i = !(e[a] = void 0);
                } catch (e) { }
                var r = s.call(e);
                return i && (t ? (e[a] = n) : delete e[a]), r;
            };
        },
        function (e, t) {
            var n = Object.prototype.toString;
            e.exports = function (e) {
                return n.call(e);
            };
        },
        function (e, t) {
            e.exports = function (e) {
                return null != e && "object" == typeof e;
            };
        },
        function (e, t, n) {
            var i = n(34);
            e.exports = function (e, t) {
                var n;
                if ("function" != typeof t) throw new TypeError("Expected a function");
                return (
                    (e = i(e)),
                    function () {
                        return (
                            0 < --e && (n = t.apply(this, arguments)),
                            e <= 1 && (t = void 0),
                            n
                        );
                    }
                );
            };
        },
        function (e, t, n) {
            var i = n(35);
            e.exports = function (e) {
                var t = i(e),
                    n = t % 1;
                return t == t ? (n ? t - n : t) : 0;
            };
        },
        function (e, t, n) {
            var i = n(8);
            e.exports = function (e) {
                return e
                    ? (e = i(e)) !== 1 / 0 && e !== -1 / 0
                        ? e == e
                            ? e
                            : 0
                        : 17976931348623157e292 * (e < 0 ? -1 : 1)
                    : 0 === e
                        ? e
                        : 0;
            };
        },
        function (e, t, n) { },
        function (e, t, n) {
            "use strict";
            n.r(t);
            var i = n(15),
                N = n.n(i),
                r = n(0),
                E = n.n(r),
                o = n(3),
                s = n.n(o),
                a = n(1),
                w = n.n(a),
                l = n(16),
                u = n.n(l),
                c = n(4),
                L = n.n(c).a;
            function d(r) {
                return function (e) {
                    if ("mousedown" === e.type && 0 === e.button) {
                        for (
                            var t = arguments.length, n = new Array(1 < t ? t - 1 : 0), i = 1;
                            i < t;
                            i++
                        )
                            n[i - 1] = arguments[i];
                        r.call.apply(r, [this, e].concat(n));
                    }
                };
            }
            var h,
                f = n(5),
                p = n.n(f),
                v = function (n, i) {
                    var r = document.createElement("_"),
                        o = r.appendChild(document.createElement("_")),
                        s = r.appendChild(document.createElement("_")),
                        e = o.appendChild(document.createElement("_")),
                        a = void 0,
                        l = void 0;
                    return (
                        (o.style.cssText = r.style.cssText =
                            "height:100%;left:0;opacity:0;overflow:hidden;pointer-events:none;position:absolute;top:0;transition:0s;width:100%;z-index:-1"),
                        (e.style.cssText = s.style.cssText =
                            "display:block;height:100%;transition:0s;width:100%"),
                        (e.style.width = e.style.height = "200%"),
                        n.appendChild(r),
                        u(),
                        function () {
                            c(), n.removeChild(r);
                        }
                    );
                    function u() {
                        c();
                        var e = n.offsetWidth,
                            t = n.offsetHeight;
                        (e === a && t === l) ||
                            ((a = e),
                                (l = t),
                                (s.style.width = 2 * e + "px"),
                                (s.style.height = 2 * t + "px"),
                                (r.scrollLeft = r.scrollWidth),
                                (r.scrollTop = r.scrollHeight),
                                (o.scrollLeft = o.scrollWidth),
                                (o.scrollTop = o.scrollHeight),
                                i({ width: e, height: t })),
                            o.addEventListener("scroll", u),
                            r.addEventListener("scroll", u);
                    }
                    function c() {
                        o.removeEventListener("scroll", u),
                            r.removeEventListener("scroll", u);
                    }
                };
            function m(e, t) {
                var n = e.indexOf(t);
                -1 !== n && e.splice(n, 1);
            }
            var g = [],
                y = 100;
            function S(e) {
                var t = e.$el,
                    n = e.listener,
                    i = e.lastWidth,
                    r = e.lastHeight,
                    o = t.offsetWidth,
                    s = t.offsetHeight;
                (i === o && r === s) ||
                    n({ width: (e.lastWidth = o), height: (e.lastHeight = s) });
            }
            function b(e, t) {
                var n = { $el: e, listener: t, lastWidth: null, lastHeight: null };
                return (
                    g.push(n),
                    S(n),
                    (h = setInterval(function () {
                        g.forEach(S);
                    }, y)),
                    function () {
                        m(g, n), g.length || (clearInterval(h), (h = null));
                    }
                );
            }
            function O(e, t) {
                var n = 9 === document.documentMode,
                    i = !0,
                    r = (n ? b : v)(e, function () {
                        return i || t.apply(void 0, arguments);
                    });
                return (i = !1), r;
            }
            function _(e, t) {
                var n = (function (e) {
                    for (
                        var t, n, i, r, o = [], s = e.parentNode;
                        s && "BODY" !== s.nodeName && s.nodeType === document.ELEMENT_NODE;

                    )
                        (t = getComputedStyle(s)),
                            (n = t.overflow),
                            (i = t.overflowX),
                            (r = t.overflowY),
                            /(auto|scroll|overlay)/.test(n + r + i) && o.push(s),
                            (s = s.parentNode);
                    return o.push(window), o;
                })(e);
                return (
                    window.addEventListener("resize", t, { passive: !0 }),
                    n.forEach(function (e) {
                        e.addEventListener("scroll", t, { passive: !0 });
                    }),
                    function () {
                        window.removeEventListener("resize", t),
                            n.forEach(function (e) {
                                e.removeEventListener("scroll", t);
                            });
                    }
                );
            }
            function C(e) {
                return e != e;
            }
            var x = n(10),
                M = n.n(x),
                I = n(11),
                D = n.n(I),
                T = n(12),
                $ = n.n(T),
                A = n(13),
                B = n.n(A),
                R = function () {
                    return Object.create(null);
                },
                z = n(17),
                V = n.n(z);
            function k(e) {
                return (
                    null != e &&
                    "object" === V()(e) &&
                    Object.getPrototypeOf(e) === Object.prototype
                );
            }
            function F(e, t) {
                if (k(t))
                    for (var n = Object.keys(t), i = 0, r = n.length; i < r; i++)
                        (o = e),
                            (s = n[i]),
                            k((a = t[n[i]])) ? (o[s] || (o[s] = {}), F(o[s], a)) : (o[s] = a);
                var o, s, a;
                return e;
            }
            var j = n(14),
                H = n.n(j);
            function P(e, t) {
                return -1 !== e.indexOf(t);
            }
            function W(e, t, n) {
                for (var i = 0, r = e.length; i < r; i++)
                    if (t.call(n, e[i], i, e)) return e[i];
            }
            function Q(e, t) {
                if (e.length !== t.length) return !0;
                for (var n = 0; n < e.length; n++) if (e[n] !== t[n]) return !0;
                return !1;
            }
            var q = null,
                K = "ALL_CHILDREN",
                X = "ALL_DESCENDANTS",
                U = "LEAF_CHILDREN",
                Y = "LEAF_DESCENDANTS",
                J = "LOAD_ROOT_OPTIONS",
                G = "LOAD_CHILDREN_OPTIONS",
                Z = "ASYNC_SEARCH",
                ee = "BRANCH_PRIORITY",
                te = "LEAF_PRIORITY",
                ne = "ALL_WITH_INDETERMINATE",
                ie = "ORDER_SELECTED",
                re = 8,
                oe = 13,
                se = 27,
                ae = 35,
                le = 36,
                ue = 37,
                ce = 38,
                de = 39,
                he = 40,
                vr = 188,
                tab = 9,
                fe = 46;
            function pe(e, t) {
                for (var n = 0; ;) {
                    if (e.level < n) return -1;
                    if (t.level < n) return 1;
                    if (e.index[n] !== t.index[n]) return e.index[n] - t.index[n];
                    n++;
                }
            }
            function ve(e, t, n) {
                return e ? u()(t, n) : P(n, t);
            }
            function me(e) {
                return e.message || String(e);
            }
            var ge = 0,
                ye = {
                    provide: function () {
                        return { instance: this };
                    },
                    props: {
                        allowClearingDisabled: { type: Boolean, default: !1 },
                        allowSelectingDisabledDescendants: { type: Boolean, default: !1 },
                        alwaysOpen: { type: Boolean, default: !1 },
                        appendToBody: { type: Boolean, default: !1 },
                        async: { type: Boolean, default: !1 },
                        autofocus: { type: Boolean, default: !1 },
                        autoFocus: { type: Boolean, default: !1 },
                        autoLoadRootOptions: { type: Boolean, default: !0 },
                        autoDeselectAncestors: { type: Boolean, default: !1 },
                        autoDeselectDescendants: { type: Boolean, default: !1 },
                        autoSelectAncestors: { type: Boolean, default: !1 },
                        autoSelectDescendants: { type: Boolean, default: !1 },
                        backspaceRemoves: { type: Boolean, default: !0 },
                        beforeClearAll: { type: Function, default: B()(!0) },
                        branchNodesFirst: { type: Boolean, default: !1 },
                        cacheOptions: { type: Boolean, default: !0 },
                        clearable: { type: Boolean, default: !0 },
                        clearAllText: { type: String, default: "Clear all" },
                        clearOnSelect: { type: Boolean, default: !1 },
                        clearValueText: { type: String, default: "Clear value" },
                        closeOnSelect: { type: Boolean, default: !0 },
                        defaultExpandLevel: { type: Number, default: 0 },
                        defaultOptions: { default: !1 },
                        deleteRemoves: { type: Boolean, default: !0 },
                        delimiter: { type: String, default: "," },
                        flattenSearchResults: { type: Boolean, default: !1 },
                        disableBranchNodes: { type: Boolean, default: !1 },
                        disabled: { type: Boolean, default: !1 },
                        disableFuzzyMatching: { type: Boolean, default: !1 },
                        flat: { type: Boolean, default: !1 },
                        id: { default: null },
                        instanceId: {
                            default: function () {
                                return "".concat(ge++, "$$");
                            },
                            type: [String, Number]
                        },
                        joinValues: { type: Boolean, default: !1 },
                        limit: { type: Number, default: 1 / 0 },
                        limitText: {
                            type: Function,
                            default: function (e) {
                                return "and ".concat(e, " more");
                            }
                        },
                        loading: { type: Boolean, default: !1 },
                        loadingText: { type: String, default: "Loading..." },
                        loadOptions: { type: Function },
                        matchKeys: { type: Array, default: B()(["label"]) },
                        maxHeight: { type: Number, default: 300 },
                        field: { type: String },
                        multiple: { type: Boolean, default: !1 },
                        isInsertable: { type: Boolean, default: !1 },
                        name: { type: String },
                        noChildrenText: { type: String, default: "No sub-options." },
                        noOptionsText: { type: String, default: "No options available." },
                        noResultsText: { type: String, default: "No results found..." },
                        normalizer: { type: Function, default: $.a },
                        openDirection: {
                            type: String,
                            default: "auto",
                            validator: function (e) {
                                return P(["auto", "top", "bottom", "above", "below"], e);
                            }
                        },
                        openOnClick: { type: Boolean, default: !0 },
                        openOnFocus: { type: Boolean, default: !1 },
                        options: { type: Array },
                        placeholder: { type: String, default: "Select..." },
                        required: { type: Boolean, default: !1 },
                        retryText: { type: String, default: "Retry?" },
                        retryTitle: { type: String, default: "Click to retry" },
                        searchable: { type: Boolean, default: !0 },
                        searchNested: { type: Boolean, default: !1 },
                        searchPromptText: { type: String, default: "Type to search..." },
                        showCount: { type: Boolean, default: !1 },
                        showCountOf: {
                            type: String,
                            default: K,
                            validator: function (e) {
                                return P([K, X, U, Y], e);
                            }
                        },
                        showCountOnSearch: null,
                        sortValueBy: {
                            type: String,
                            default: ie,
                            validator: function (e) {
                                return P([ie, "LEVEL", "INDEX"], e);
                            }
                        },
                        tabIndex: { type: Number, default: 0 },
                        value: null,
                        valueConsistsOf: {
                            type: String,
                            default: ee,
                            validator: function (e) {
                                return P(["ALL", ee, te, ne], e);
                            }
                        },
                        valueFormat: { type: String, default: "id" },
                        zIndex: { type: [Number, String], default: 999 }
                    },
                    data: function () {
                        return {
                            trigger: { isFocused: !1, searchQuery: "" },
                            menu: {
                                isOpen: !1,
                                current: null,
                                lastScrollPosition: 0,
                                placement: "bottom"
                            },
                            forest: {
                                normalizedOptions: [],
                                nodeMap: R(),
                                checkedStateMap: R(),
                                selectedNodeIds: this.extractCheckedNodeIdsFromValue(),
                                selectedNodeMap: R()
                            },
                            rootOptionsStates: {
                                isLoaded: !1,
                                isLoading: !1,
                                loadingError: ""
                            },
                            localSearch: { active: !1, noResults: !0, countMap: R() },
                            remoteSearch: R()
                        };
                    },
                    computed: {
                        selectedNodes: function () {
                            return this.forest.selectedNodeIds.map(this.getNode);
                        },
                        internalValue: function () {
                            var t,
                                r = this;
                            if (
                                this.single ||
                                this.flat ||
                                this.disableBranchNodes ||
                                "ALL" === this.valueConsistsOf
                            )
                                t = this.forest.selectedNodeIds.slice();
                            else if (this.valueConsistsOf === ee)
                                t = this.forest.selectedNodeIds.filter(function (e) {
                                    var t = r.getNode(e);
                                    return !!t.isRootNode || !r.isSelected(t.parentNode);
                                });
                            else if (this.valueConsistsOf === te)
                                t = this.forest.selectedNodeIds.filter(function (e) {
                                    var t = r.getNode(e);
                                    return !!t.isLeaf || 0 === t.children.length;
                                });
                            else if (this.valueConsistsOf === ne) {
                                var e,
                                    n = [];
                                (t = this.forest.selectedNodeIds.slice()),
                                    this.selectedNodes.forEach(function (e) {
                                        e.ancestors.forEach(function (e) {
                                            P(n, e.id) || P(t, e.id) || n.push(e.id);
                                        });
                                    }),
                                    (e = t).push.apply(e, n);
                            }
                            return (
                                "LEVEL" === this.sortValueBy
                                    ? t.sort(function (e, t) {
                                        return (
                                            (n = r.getNode(e)),
                                            (i = r.getNode(t)),
                                            n.level === i.level ? pe(n, i) : n.level - i.level
                                        );
                                        var n, i;
                                    })
                                    : "INDEX" === this.sortValueBy &&
                                    t.sort(function (e, t) {
                                        return pe(r.getNode(e), r.getNode(t));
                                    }),
                                t
                            );
                        },
                        hasValue: function () {
                            return 0 < this.internalValue.length;
                        },
                        single: function () {
                            return !this.multiple;
                        },
                        visibleOptionIds: function () {
                            var t = this,
                                n = [];
                            return (
                                this.traverseAllNodesByIndex(function (e) {
                                    if (
                                        ((t.localSearch.active &&
                                            !t.shouldOptionBeIncludedInSearchResult(e)) ||
                                            n.push(e.id),
                                            e.isBranch && !t.shouldExpand(e))
                                    )
                                        return !1;
                                }),
                                n
                            );
                        },
                        hasVisibleOptions: function () {
                            return 0 !== this.visibleOptionIds.length;
                        },
                        showCountOnSearchComputed: function () {
                            return "boolean" == typeof this.showCountOnSearch
                                ? this.showCountOnSearch
                                : this.showCount;
                        },
                        hasBranchNodes: function () {
                            return this.forest.normalizedOptions.some(function (e) {
                                return e.isBranch;
                            });
                        },
                        shouldFlattenOptions: function () {
                            return this.localSearch.active && this.flattenSearchResults;
                        }
                    },
                    watch: {
                        alwaysOpen: function (e) {
                            e ? this.openMenu() : this.closeMenu();
                        },
                        branchNodesFirst: function () {
                            this.initialize();
                        },
                        disabled: function (e) {
                            e && this.menu.isOpen
                                ? this.closeMenu()
                                : e || this.menu.isOpen || !this.alwaysOpen || this.openMenu();
                        },
                        flat: function () {
                            this.initialize();
                        },
                        internalValue: function (e, t) {
                            Q(e, t) &&
                                this.$emit("input", this.getValue(), this.getInstanceId());
                        },
                        matchKeys: function () {
                            this.initialize();
                        },
                        multiple: function (e) {
                            e && this.buildForestState();
                        },
                        options: {
                            handler: function () {
                                this.async ||
                                    (this.initialize(),
                                        (this.rootOptionsStates.isLoaded = Array.isArray(
                                            this.options
                                        )));
                            },
                            deep: !0,
                            immediate: !0
                        },
                        "trigger.searchQuery": function () {
                            this.async ? this.handleRemoteSearch() : this.handleLocalSearch(),
                                this.$emit(
                                    "search-change",
                                    this.trigger.searchQuery,
                                    this.getInstanceId()
                                );
                        },
                        value: function () {
                            var e = this.extractCheckedNodeIdsFromValue();
                            Q(e, this.internalValue) && this.fixSelectedNodeIds(e);
                        }
                    },
                    methods: {
                        verifyProps: function () {
                            var t = this;
                            if (
                                (L(
                                    function () {
                                        return null == t.id;
                                    },
                                    function () {
                                        return "`id` prop is deprecated. Use `instanceId` instead.";
                                    }
                                ),
                                    L(
                                        function () {
                                            return !t.autofocus;
                                        },
                                        function () {
                                            return "`autofocus` prop is deprecated. Use `autoFocus` instead.";
                                        }
                                    ),
                                    L(
                                        function () {
                                            return !t.async || t.searchable;
                                        },
                                        function () {
                                            return 'For async search mode, the value of "searchable" prop must be true.';
                                        }
                                    ),
                                    null != this.options ||
                                    this.loadOptions ||
                                    L(
                                        function () {
                                            return !1;
                                        },
                                        function () {
                                            return 'Are you meant to dynamically load options? You need to use "loadOptions" prop.';
                                        }
                                    ),
                                    this.flat &&
                                    L(
                                        function () {
                                            return t.multiple;
                                        },
                                        function () {
                                            return 'You are using flat mode. But you forgot to add "multiple=true"?';
                                        }
                                    ),
                                    !this.flat)
                            ) {
                                [
                                    "autoSelectAncestors",
                                    "autoSelectDescendants",
                                    "autoDeselectAncestors",
                                    "autoDeselectDescendants"
                                ].forEach(function (e) {
                                    L(
                                        function () {
                                            return !t[e];
                                        },
                                        function () {
                                            return '"'.concat(e, '" only applies to flat mode.');
                                        }
                                    );
                                });
                            }
                        },
                        resetFlags: function () {
                            this._blurOnSelect = !1;
                        },
                        initialize: function () {
                            var e = this.async
                                ? this.getRemoteSearchEntry().options
                                : this.options;
                            if (Array.isArray(e)) {
                                var t = this.forest.nodeMap;
                                (this.forest.nodeMap = R()),
                                    this.keepDataOfSelectedNodes(t),
                                    (this.forest.normalizedOptions = this.normalize(q, e, t)),
                                    this.fixSelectedNodeIds(this.internalValue);
                            } else this.forest.normalizedOptions = [];
                        },
                        getInstanceId: function () {
                            return null == this.instanceId ? this.id : this.instanceId;
                        },
                        getValue: function () {
                            var t = this;
                            if ("id" === this.valueFormat)
                                return this.multiple
                                    ? this.internalValue.slice()
                                    : this.internalValue[0];
                            var e = this.internalValue.map(function (e) {
                                return t.getNode(e).raw;
                            });
                            return this.multiple ? e : e[0];
                        },
                        getNode: function (e) {
                            return (
                                L(
                                    function () {
                                        return null != e;
                                    },
                                    function () {
                                        return "Invalid node id: ".concat(e);
                                    }
                                ),
                                null == e
                                    ? null
                                    : e in this.forest.nodeMap
                                        ? this.forest.nodeMap[e]
                                        : this.createFallbackNode(e)
                            );
                        },
                        createFallbackNode: function (e) {
                            var t = this.extractNodeFromValue(e),
                                n = {
                                    id: e,
                                    label:
                                        this.enhancedNormalizer(t).label ||
                                        "".concat(e, " (unknown)"),
                                    ancestors: [],
                                    parentNode: q,
                                    isFallbackNode: !0,
                                    isRootNode: !0,
                                    isLeaf: !0,
                                    isBranch: !1,
                                    isDisabled: !1,
                                    isNew: !1,
                                    index: [-1],
                                    level: 0,
                                    raw: t
                                };
                            return this.$set(this.forest.nodeMap, e, n);
                        },
                        extractCheckedNodeIdsFromValue: function () {
                            var t = this;
                            return null == this.value
                                ? []
                                : "id" === this.valueFormat
                                    ? this.multiple
                                        ? this.value.slice()
                                        : [this.value]
                                    : (this.multiple ? this.value : [this.value])
                                        .map(function (e) {
                                            return t.enhancedNormalizer(e);
                                        })
                                        .map(function (e) {
                                            return e.id;
                                        });
                        },
                        extractNodeFromValue: function (t) {
                            var n = this,
                                e = { id: t };
                            return "id" === this.valueFormat
                                ? e
                                : W(
                                    this.multiple
                                        ? Array.isArray(this.value)
                                            ? this.value
                                            : []
                                        : this.value
                                            ? [this.value]
                                            : [],
                                    function (e) {
                                        return e && n.enhancedNormalizer(e).id === t;
                                    }
                                ) || e;
                        },
                        fixSelectedNodeIds: function (e) {
                            var n = this,
                                i = [];
                            if (
                                this.single ||
                                this.flat ||
                                this.disableBranchNodes ||
                                "ALL" === this.valueConsistsOf
                            )
                                i = e;
                            else if (this.valueConsistsOf === ee)
                                e.forEach(function (e) {
                                    i.push(e);
                                    var t = n.getNode(e);
                                    t.isBranch &&
                                        n.traverseDescendantsBFS(t, function (e) {
                                            i.push(e.id);
                                        });
                                });
                            else if (this.valueConsistsOf === te)
                                for (var t = R(), r = e.slice(); r.length;) {
                                    var o = r.shift(),
                                        s = this.getNode(o);
                                    i.push(o),
                                        s.isRootNode ||
                                        (s.parentNode.id in t ||
                                            (t[s.parentNode.id] = s.parentNode.children.length),
                                            0 == --t[s.parentNode.id] && r.push(s.parentNode.id));
                                }
                            else if (this.valueConsistsOf === ne)
                                for (
                                    var a = R(),
                                    l = e.filter(function (e) {
                                        var t = n.getNode(e);
                                        return t.isLeaf || 0 === t.children.length;
                                    });
                                    l.length;

                                ) {
                                    var u = l.shift(),
                                        c = this.getNode(u);
                                    i.push(u),
                                        c.isRootNode ||
                                        (c.parentNode.id in a ||
                                            (a[c.parentNode.id] = c.parentNode.children.length),
                                            0 == --a[c.parentNode.id] && l.push(c.parentNode.id));
                                }
                            Q(this.forest.selectedNodeIds, i) &&
                                (this.forest.selectedNodeIds = i),
                                this.buildForestState();
                        },
                        keepDataOfSelectedNodes: function (n) {
                            var i = this;
                            this.forest.selectedNodeIds.forEach(function (e) {
                                if (n[e]) {
                                    var t = w()({}, n[e], { isFallbackNode: !0 });
                                    i.$set(i.forest.nodeMap, e, t);
                                }
                            });
                        },
                        isSelected: function (e) {
                            return !0 === this.forest.selectedNodeMap[e.id];
                        },
                        traverseDescendantsBFS: function (e, t) {
                            if (e.isBranch)
                                for (var n = e.children.slice(); n.length;) {
                                    var i = n[0];
                                    i.isBranch && n.push.apply(n, s()(i.children)),
                                        t(i),
                                        n.shift();
                                }
                        },
                        traverseDescendantsDFS: function (e, t) {
                            var n = this;
                            e.isBranch &&
                                e.children.forEach(function (e) {
                                    n.traverseDescendantsDFS(e, t), t(e);
                                });
                        },
                        traverseAllNodesDFS: function (t) {
                            var n = this;
                            this.forest.normalizedOptions.forEach(function (e) {
                                n.traverseDescendantsDFS(e, t), t(e);
                            });
                        },
                        traverseAllNodesByIndex: function (n) {
                            !(function t(e) {
                                e.children.forEach(function (e) {
                                    !1 !== n(e) && e.isBranch && t(e);
                                });
                            })({ children: this.forest.normalizedOptions });
                        },
                        toggleClickOutsideEvent: function (e) {
                            e
                                ? document.addEventListener(
                                    "mousedown",
                                    this.handleClickOutside,
                                    !1
                                )
                                : document.removeEventListener(
                                    "mousedown",
                                    this.handleClickOutside,
                                    !1
                                );
                        },
                        getValueContainer: function () {
                            return this.$refs.control.$refs["value-container"];
                        },
                        getInput: function () {
                            return this.getValueContainer().$refs.input;
                        },
                        focusInput: function () {
                            this.getInput().focus();
                        },
                        blurInput: function () {
                            this.getInput().blur();
                        },
                        handleMouseDown: d(function (e) {
                            (e.preventDefault(), e.stopPropagation(), this.disabled) ||
                                (this.getValueContainer().$el.contains(e.target) &&
                                    !this.menu.isOpen &&
                                    (this.openOnClick || this.trigger.isFocused) &&
                                    this.openMenu(),
                                    this._blurOnSelect ? this.blurInput() : this.focusInput(),
                                    this.resetFlags());
                        }),
                        handleClickOutside: function (e) {
                            this.$refs.wrapper &&
                                !this.$refs.wrapper.contains(e.target) &&
                                (this.blurInput(), this.closeMenu());
                        },
                        handleLocalSearch: function () {
                            var n = this,
                                e = this.trigger.searchQuery,
                                t = function () {
                                    return n.resetHighlightedOptionWhenNecessary(!0);
                                };
                            if (!e) return (this.localSearch.active = !1), t();
                            (this.localSearch.active = !0),
                                (this.localSearch.noResults = !0),
                                this.traverseAllNodesDFS(function (e) {
                                    var t;
                                    e.isBranch &&
                                        ((e.isExpandedOnSearch = !1),
                                            (e.showAllChildrenOnSearch = !1),
                                            (e.isMatched = !1),
                                            (e.hasMatchedDescendants = !1),
                                            n.$set(
                                                n.localSearch.countMap,
                                                e.id,
                                                ((t = {}),
                                                    E()(t, K, 0),
                                                    E()(t, X, 0),
                                                    E()(t, U, 0),
                                                    E()(t, Y, 0),
                                                    t)
                                            ));
                                });
                            var i = e.trim().toLocaleLowerCase(),
                                r = i.replace(/\s+/g, " ").split(" ");
                            this.traverseAllNodesDFS(function (t) {
                                n.searchNested && 1 < r.length
                                    ? (t.isMatched = r.every(function (e) {
                                        return ve(!1, e, t.nestedSearchLabel);
                                    }))
                                    : (t.isMatched = n.matchKeys.some(function (e) {
                                        return ve(!n.disableFuzzyMatching, i, t.lowerCased[e]);
                                    })),
                                    t.isMatched &&
                                    ((n.localSearch.noResults = !1),
                                        t.ancestors.forEach(function (e) {
                                            return n.localSearch.countMap[e.id][X]++;
                                        }),
                                        t.isLeaf &&
                                        t.ancestors.forEach(function (e) {
                                            return n.localSearch.countMap[e.id][Y]++;
                                        }),
                                        t.parentNode !== q &&
                                        ((n.localSearch.countMap[t.parentNode.id][K] += 1),
                                            t.isLeaf &&
                                            (n.localSearch.countMap[t.parentNode.id][U] += 1))),
                                    (t.isMatched || (t.isBranch && t.isExpandedOnSearch)) &&
                                    t.parentNode !== q &&
                                    ((t.parentNode.isExpandedOnSearch = !0),
                                        (t.parentNode.hasMatchedDescendants = !0));
                            }),
                                t();
                        },
                        handleRemoteSearch: function () {
                            var t = this,
                                n = this.trigger.searchQuery,
                                i = this.getRemoteSearchEntry(),
                                r = function () {
                                    t.initialize(), t.resetHighlightedOptionWhenNecessary(!0);
                                };
                            if (("" === n || this.cacheOptions) && i.isLoaded) return r();
                            this.callLoadOptionsProp({
                                action: Z,
                                args: { searchQuery: n },
                                isPending: function () {
                                    return i.isLoading;
                                },
                                start: function () {
                                    (i.isLoading = !0), (i.isLoaded = !1), (i.loadingError = "");
                                },
                                succeed: function (e) {
                                    (i.isLoaded = !0),
                                        (i.options = e),
                                        t.trigger.searchQuery === n && r();
                                },
                                fail: function (e) {
                                    i.loadingError = me(e);
                                },
                                end: function () {
                                    i.isLoading = !1;
                                }
                            });
                        },
                        getRemoteSearchEntry: function () {
                            var e = this,
                                t = this.trigger.searchQuery,
                                n =
                                    this.remoteSearch[t] ||
                                    w()(
                                        {},
                                        { isLoaded: !1, isLoading: !1, loadingError: "" },
                                        { options: [] }
                                    );
                            if (
                                (this.$watch(
                                    function () {
                                        return n.options;
                                    },
                                    function () {
                                        e.trigger.searchQuery === t && e.initialize();
                                    },
                                    { deep: !0 }
                                ),
                                    "" === t)
                            ) {
                                if (Array.isArray(this.defaultOptions))
                                    return (
                                        (n.options = this.defaultOptions), (n.isLoaded = !0), n
                                    );
                                if (!0 !== this.defaultOptions) return (n.isLoaded = !0), n;
                            }
                            return (
                                this.remoteSearch[t] || this.$set(this.remoteSearch, t, n), n
                            );
                        },
                        shouldExpand: function (e) {
                            return this.localSearch.active
                                ? e.isExpandedOnSearch
                                : e.isExpanded;
                        },
                        shouldOptionBeIncludedInSearchResult: function (e) {
                            return (
                                !!e.isMatched ||
                                (!(
                                    !e.isBranch ||
                                    !e.hasMatchedDescendants ||
                                    this.flattenSearchResults
                                ) ||
                                    !(e.isRootNode || !e.parentNode.showAllChildrenOnSearch))
                            );
                        },
                        shouldShowOptionInMenu: function (e) {
                            return !(
                                this.localSearch.active &&
                                !this.shouldOptionBeIncludedInSearchResult(e)
                            );
                        },
                        getControl: function () {
                            return this.$refs.control.$el;
                        },
                        getMenu: function () {
                            var e = (this.appendToBody
                                ? this.$refs.portal.portalTarget
                                : this
                            ).$refs.menu.$refs.menu;
                            return e && "#comment" !== e.nodeName ? e : null;
                        },
                        setCurrentHighlightedOption: function (a) {
                            var l = this,
                                e =
                                    !(1 < arguments.length && void 0 !== arguments[1]) ||
                                    arguments[1],
                                t = this.menu.current;
                            if (
                                (null != t &&
                                    t in this.forest.nodeMap &&
                                    (this.forest.nodeMap[t].isHighlighted = !1),
                                    (this.menu.current = a.id),
                                    (a.isHighlighted = !0),
                                    this.menu.isOpen && e)
                            ) {
                                var n = function () {
                                    var e,
                                        t,
                                        n,
                                        i,
                                        r,
                                        o = l.getMenu(),
                                        s = o.querySelector(
                                            '.vue-treeselect__option[data-id="'.concat(a.id, '"]')
                                        );
                                    s &&
                                        ((t = s),
                                            (n = (e = o).getBoundingClientRect()),
                                            (i = t.getBoundingClientRect()),
                                            (r = t.offsetHeight / 3),
                                            i.bottom + r > n.bottom
                                                ? (e.scrollTop = Math.min(
                                                    t.offsetTop + t.clientHeight - e.offsetHeight + r,
                                                    e.scrollHeight
                                                ))
                                                : i.top - r < n.top &&
                                                (e.scrollTop = Math.max(t.offsetTop - r, 0)));
                                };
                                this.getMenu() ? n() : this.$nextTick(n);
                            }
                        },
                        resetHighlightedOptionWhenNecessary: function () {
                            var e =
                                0 < arguments.length &&
                                void 0 !== arguments[0] &&
                                arguments[0],
                                t = this.menu.current;
                            (!e &&
                                null != t &&
                                t in this.forest.nodeMap &&
                                this.shouldShowOptionInMenu(this.getNode(t))) ||
                                this.highlightFirstOption();
                        },
                        highlightFirstOption: function () {
                            if (this.hasVisibleOptions) {
                                var e = this.visibleOptionIds[0];
                                this.setCurrentHighlightedOption(this.getNode(e));
                            }
                        },
                        highlightPrevOption: function () {
                            if (this.hasVisibleOptions) {
                                var e = this.visibleOptionIds.indexOf(this.menu.current) - 1;
                                if (-1 === e) return this.highlightLastOption();
                                this.setCurrentHighlightedOption(
                                    this.getNode(this.visibleOptionIds[e])
                                );
                            }
                        },
                        highlightNextOption: function () {
                            if (this.hasVisibleOptions) {
                                var e = this.visibleOptionIds.indexOf(this.menu.current) + 1;
                                if (e === this.visibleOptionIds.length)
                                    return this.highlightFirstOption();
                                this.setCurrentHighlightedOption(
                                    this.getNode(this.visibleOptionIds[e])
                                );
                            }
                        },
                        highlightLastOption: function () {
                            if (this.hasVisibleOptions) {
                                var e = H()(this.visibleOptionIds);
                                this.setCurrentHighlightedOption(this.getNode(e));
                            }
                        },
                        resetSearchQuery: function () {
                            this.trigger.searchQuery = "";
                        },
                        closeMenu: function () {
                            !this.menu.isOpen ||
                                (!this.disabled && this.alwaysOpen) ||
                                (this.saveMenuScrollPosition(),
                                    (this.menu.isOpen = !1),
                                    this.toggleClickOutsideEvent(!1),
                                    this.resetSearchQuery(),
                                    this.$emit("close", this.getValue(), this.getInstanceId()));
                        },
                        openMenu: function () {
                            this.disabled ||
                                this.menu.isOpen ||
                                ((this.menu.isOpen = !0),
                                    this.$nextTick(this.resetHighlightedOptionWhenNecessary),
                                    this.$nextTick(this.restoreMenuScrollPosition),
                                    this.options || this.async || this.loadRootOptions(),
                                    this.toggleClickOutsideEvent(!0),
                                    this.$emit("open", this.getInstanceId()));
                        },
                        toggleMenu: function () {
                            this.menu.isOpen ? this.closeMenu() : this.openMenu();
                        },
                        toggleExpanded: function (e) {
                            var t;
                            this.localSearch.active
                                ? (t = e.isExpandedOnSearch = !e.isExpandedOnSearch) &&
                                (e.showAllChildrenOnSearch = !0)
                                : (t = e.isExpanded = !e.isExpanded),
                                t && !e.childrenStates.isLoaded && this.loadChildrenOptions(e);
                        },
                        buildForestState: function () {
                            var t = this,
                                n = R();
                            this.forest.selectedNodeIds.forEach(function (e) {
                                n[e] = !0;
                            }),
                                (this.forest.selectedNodeMap = n);
                            var i = R();
                            this.multiple &&
                                (this.traverseAllNodesByIndex(function (e) {
                                    i[e.id] = 0;
                                }),
                                    this.selectedNodes.forEach(function (e) {
                                        (i[e.id] = 2),
                                            t.flat ||
                                            t.disableBranchNodes ||
                                            e.ancestors.forEach(function (e) {
                                                t.isSelected(e) || (i[e.id] = 1);
                                            });
                                    })),
                                (this.forest.checkedStateMap = i);
                        },
                        enhancedNormalizer: function (e) {
                            return w()({}, e, this.normalizer(e, this.getInstanceId()));
                        },
                        normalize: function (O, e, _) {
                            var x = this,
                                t = e
                                    .map(function (e) {
                                        return [x.enhancedNormalizer(e), e];
                                    })
                                    .map(function (e, t) {
                                        var n = N()(e, 2),
                                            i = n[0],
                                            r = n[1];
                                        x.checkDuplication(i), x.verifyNodeShape(i);
                                        var o = i.id,
                                            s = i.label,
                                            a = i.children,
                                            l = i.isDefaultExpanded,
                                            u = O === q,
                                            c = u ? 0 : O.level + 1,
                                            d = Array.isArray(a) || null === a,
                                            h = !d,
                                            f = !!i.isDisabled || (!x.flat && !u && O.isDisabled),
                                            p = !!i.isNew,
                                            v = x.matchKeys.reduce(function (e, t) {
                                                return w()(
                                                    {},
                                                    e,
                                                    E()(
                                                        {},
                                                        t,
                                                        ((n = i[t]),
                                                            "string" == typeof n
                                                                ? n
                                                                : "number" != typeof n || C(n)
                                                                    ? ""
                                                                    : n + "").toLocaleLowerCase()
                                                    )
                                                );
                                                var n;
                                            }, {}),
                                            m = u ? v.label : O.nestedSearchLabel + " " + v.label,
                                            g = x.$set(x.forest.nodeMap, o, R());
                                        if (
                                            (x.$set(g, "id", o),
                                                x.$set(g, "label", s),
                                                x.$set(g, "level", c),
                                                x.$set(g, "ancestors", u ? [] : [O].concat(O.ancestors)),
                                                x.$set(g, "index", (u ? [] : O.index).concat(t)),
                                                x.$set(g, "parentNode", O),
                                                x.$set(g, "lowerCased", v),
                                                x.$set(g, "nestedSearchLabel", m),
                                                x.$set(g, "isDisabled", f),
                                                x.$set(g, "isNew", p),
                                                x.$set(g, "isMatched", !1),
                                                x.$set(g, "isHighlighted", !1),
                                                x.$set(g, "isBranch", d),
                                                x.$set(g, "isLeaf", h),
                                                x.$set(g, "isRootNode", u),
                                                x.$set(g, "raw", r),
                                                d)
                                        ) {
                                            var y,
                                                S = Array.isArray(a);
                                            x.$set(
                                                g,
                                                "childrenStates",
                                                w()(
                                                    {},
                                                    { isLoaded: !1, isLoading: !1, loadingError: "" },
                                                    { isLoaded: S }
                                                )
                                            ),
                                                x.$set(
                                                    g,
                                                    "isExpanded",
                                                    "boolean" == typeof l ? l : c < x.defaultExpandLevel
                                                ),
                                                x.$set(g, "hasMatchedDescendants", !1),
                                                x.$set(g, "hasDisabledDescendants", !1),
                                                x.$set(g, "isExpandedOnSearch", !1),
                                                x.$set(g, "showAllChildrenOnSearch", !1),
                                                x.$set(
                                                    g,
                                                    "count",
                                                    ((y = {}),
                                                        E()(y, K, 0),
                                                        E()(y, X, 0),
                                                        E()(y, U, 0),
                                                        E()(y, Y, 0),
                                                        y)
                                                ),
                                                x.$set(g, "children", S ? x.normalize(g, a, _) : []),
                                                !0 === l &&
                                                g.ancestors.forEach(function (e) {
                                                    e.isExpanded = !0;
                                                }),
                                                S || "function" == typeof x.loadOptions
                                                    ? !S && g.isExpanded && x.loadChildrenOptions(g)
                                                    : L(
                                                        function () {
                                                            return !1;
                                                        },
                                                        function () {
                                                            return 'Unloaded branch node detected. "loadOptions" prop is required to load its children.';
                                                        }
                                                    );
                                        }
                                        if (
                                            (g.ancestors.forEach(function (e) {
                                                return e.count[X]++;
                                            }),
                                                h &&
                                                g.ancestors.forEach(function (e) {
                                                    return e.count[Y]++;
                                                }),
                                                u ||
                                                ((O.count[K] += 1),
                                                    h && (O.count[U] += 1),
                                                    f && (O.hasDisabledDescendants = !0)),
                                                _ && _[o])
                                        ) {
                                            var b = _[o];
                                            (g.isMatched = b.isMatched),
                                                (g.showAllChildrenOnSearch = b.showAllChildrenOnSearch),
                                                (g.isHighlighted = b.isHighlighted),
                                                b.isBranch &&
                                                g.isBranch &&
                                                ((g.isExpanded = b.isExpanded),
                                                    (g.isExpandedOnSearch = b.isExpandedOnSearch),
                                                    b.childrenStates.isLoaded &&
                                                        !g.childrenStates.isLoaded
                                                        ? (g.isExpanded = !1)
                                                        : (g.childrenStates = w()({}, b.childrenStates)));
                                        }
                                        return g;
                                    });
                            if (this.branchNodesFirst) {
                                var n = t.filter(function (e) {
                                    return e.isBranch;
                                }),
                                    i = t.filter(function (e) {
                                        return e.isLeaf;
                                    });
                                t = n.concat(i);
                            }
                            return t;
                        },
                        loadRootOptions: function () {
                            var t = this;
                            this.callLoadOptionsProp({
                                action: J,
                                isPending: function () {
                                    return t.rootOptionsStates.isLoading;
                                },
                                start: function () {
                                    (t.rootOptionsStates.isLoading = !0),
                                        (t.rootOptionsStates.loadingError = "");
                                },
                                succeed: function () {
                                    (t.rootOptionsStates.isLoaded = !0),
                                        t.$nextTick(function () {
                                            t.resetHighlightedOptionWhenNecessary(!0);
                                        });
                                },
                                fail: function (e) {
                                    t.rootOptionsStates.loadingError = me(e);
                                },
                                end: function () {
                                    t.rootOptionsStates.isLoading = !1;
                                }
                            });
                        },
                        loadChildrenOptions: function (e) {
                            var t = this,
                                n = e.id,
                                i = e.raw;
                            this.callLoadOptionsProp({
                                action: G,
                                args: { parentNode: i },
                                isPending: function () {
                                    return t.getNode(n).childrenStates.isLoading;
                                },
                                start: function () {
                                    (t.getNode(n).childrenStates.isLoading = !0),
                                        (t.getNode(n).childrenStates.loadingError = "");
                                },
                                succeed: function () {
                                    t.getNode(n).childrenStates.isLoaded = !0;
                                },
                                fail: function (e) {
                                    t.getNode(n).childrenStates.loadingError = me(e);
                                },
                                end: function () {
                                    t.getNode(n).childrenStates.isLoading = !1;
                                }
                            });
                        },
                        callLoadOptionsProp: function (e) {
                            var t = e.action,
                                n = e.args,
                                i = e.isPending,
                                r = e.start,
                                o = e.succeed,
                                s = e.fail,
                                a = e.end;
                            if (this.loadOptions && !i()) {
                                r();
                                var l = D()(function (e, t) {
                                    e ? s(e) : o(t), a();
                                }),
                                    u = this.loadOptions(
                                        w()(
                                            {
                                                id: this.getInstanceId(),
                                                instanceId: this.getInstanceId(),
                                                action: t
                                            },
                                            n,
                                            { callback: l }
                                        )
                                    );
                                M()(u) &&
                                    u
                                        .then(
                                            function () {
                                                l();
                                            },
                                            function (e) {
                                                l(e);
                                            }
                                        )
                                        .catch(function (e) {
                                            console.error(e);
                                        });
                            }
                        },
                        checkDuplication: function (e) {
                            var t = this;
                            L(
                                function () {
                                    return !(
                                        e.id in t.forest.nodeMap &&
                                        !t.forest.nodeMap[e.id].isFallbackNode
                                    );
                                },
                                function () {
                                    return (
                                        "Detected duplicate presence of node id ".concat(
                                            JSON.stringify(e.id),
                                            ". "
                                        ) +
                                        'Their labels are "'
                                            .concat(t.forest.nodeMap[e.id].label, '" and "')
                                            .concat(e.label, '" respectively.')
                                    );
                                }
                            );
                        },
                        verifyNodeShape: function (e) {
                            L(
                                function () {
                                    return !(void 0 === e.children && !0 === e.isBranch);
                                },
                                function () {
                                    return "Are you meant to declare an unloaded branch node? `isBranch: true` is no longer supported, please use `children: null` instead.";
                                }
                            );
                        },
                        select: function (e) {
                            if (!this.disabled && !e.isDisabled) {
                                this.single && this.clear();
                                var t =
                                    this.multiple && !this.flat
                                        ? 0 === this.forest.checkedStateMap[e.id]
                                        : !this.isSelected(e);
                                t ? this._selectNode(e) : this._deselectNode(e),
                                    this.buildForestState(),
                                    t
                                        ? this.$emit("select", e.raw, this.getInstanceId())
                                        : this.$emit("deselect", e.raw, this.getInstanceId()),
                                    this.localSearch.active &&
                                    t &&
                                    (this.single || this.clearOnSelect) &&
                                    this.resetSearchQuery(),
                                    this.single &&
                                    this.closeOnSelect &&
                                    (this.closeMenu(),
                                        this.searchable && (this._blurOnSelect = !0));
                            }
                        },
                        clear: function () {
                            var t = this;
                            this.hasValue &&
                                (this.single || this.allowClearingDisabled
                                    ? (this.forest.selectedNodeIds = [])
                                    : (this.forest.selectedNodeIds = this.forest.selectedNodeIds.filter(
                                        function (e) {
                                            return t.getNode(e).isDisabled;
                                        }
                                    )),
                                    this.buildForestState());
                        },
                        _selectNode: function (e) {
                            var t = this;
                            if (this.single || this.disableBranchNodes)
                                return this.addValue(e);
                            if (this.flat)
                                return (
                                    this.addValue(e),
                                    void (this.autoSelectAncestors
                                        ? e.ancestors.forEach(function (e) {
                                            t.isSelected(e) || e.isDisabled || t.addValue(e);
                                        })
                                        : this.autoSelectDescendants &&
                                        this.traverseDescendantsBFS(e, function (e) {
                                            t.isSelected(e) || e.isDisabled || t.addValue(e);
                                        }))
                                );
                            var n =
                                e.isLeaf ||
                                !e.hasDisabledDescendants ||
                                this.allowSelectingDisabledDescendants;
                            if (
                                (n && this.addValue(e),
                                    e.isBranch &&
                                    this.traverseDescendantsBFS(e, function (e) {
                                        (e.isDisabled && !t.allowSelectingDisabledDescendants) ||
                                            t.addValue(e);
                                    }),
                                    n)
                            )
                                for (
                                    var i = e;
                                    (i = i.parentNode) !== q && i.children.every(this.isSelected);

                                )
                                    this.addValue(i);
                        },
                        _deselectNode: function (e) {
                            var t = this;
                            if (this.disableBranchNodes) return this.removeValue(e);
                            if (this.flat)
                                return (
                                    this.removeValue(e),
                                    void (this.autoDeselectAncestors
                                        ? e.ancestors.forEach(function (e) {
                                            t.isSelected(e) && !e.isDisabled && t.removeValue(e);
                                        })
                                        : this.autoDeselectDescendants &&
                                        this.traverseDescendantsBFS(e, function (e) {
                                            t.isSelected(e) && !e.isDisabled && t.removeValue(e);
                                        }))
                                );
                            var n = !1;
                            if (
                                (e.isBranch &&
                                    this.traverseDescendantsDFS(e, function (e) {
                                        (e.isDisabled && !t.allowSelectingDisabledDescendants) ||
                                            (t.removeValue(e), (n = !0));
                                    }),
                                    e.isLeaf || n || 0 === e.children.length)
                            ) {
                                this.removeValue(e);
                                for (
                                    var i = e;
                                    (i = i.parentNode) !== q && this.isSelected(i);

                                )
                                    this.removeValue(i);
                            }
                        },
                        addValue: function (e) {
                            this.forest.selectedNodeIds.push(e.id),
                                (this.forest.selectedNodeMap[e.id] = !0);
                        },
                        removeValue: function (e) {
                            m(this.forest.selectedNodeIds, e.id),
                                delete this.forest.selectedNodeMap[e.id];
                        },
                        removeLastValue: function () {
                            if (this.hasValue) {
                                if (this.single) return this.clear();
                                var e = H()(this.internalValue),
                                    t = this.getNode(e);
                                this.select(t);
                            }
                        },
                        saveMenuScrollPosition: function () {
                            var e = this.getMenu();
                            e && (this.menu.lastScrollPosition = e.scrollTop);
                        },
                        restoreMenuScrollPosition: function () {
                            var e = this.getMenu();
                            e && (e.scrollTop = this.menu.lastScrollPosition);
                        }
                    },
                    created: function () {
                        this.verifyProps(), this.resetFlags();
                    },
                    mounted: function () {
                        (this.autoFocus || this.autofocus) && this.focusInput(),
                            this.options ||
                            this.async ||
                            !this.autoLoadRootOptions ||
                            this.loadRootOptions(),
                            this.alwaysOpen && this.openMenu(),
                            this.async && this.defaultOptions && this.handleRemoteSearch();
                    },
                    destroyed: function () {
                        this.toggleClickOutsideEvent(!1);
                    }
                };
            function Se(e) {
                return "string" == typeof e
                    ? e
                    : null == e || C(e)
                        ? ""
                        : JSON.stringify(e);
            }
            function be(e, t, n, i, r, o, s, a) {
                var l,
                    u = "function" == typeof e ? e.options : e;
                if (
                    (t && ((u.render = t), (u.staticRenderFns = n), (u._compiled = !0)),
                        i && (u.functional = !0),
                        o && (u._scopeId = "data-v-" + o),
                        s
                            ? ((l = function (e) {
                                (e =
                                    e ||
                                    (this.$vnode && this.$vnode.ssrContext) ||
                                    (this.parent &&
                                        this.parent.$vnode &&
                                        this.parent.$vnode.ssrContext)) ||
                                    "undefined" == typeof __VUE_SSR_CONTEXT__ ||
                                    (e = __VUE_SSR_CONTEXT__),
                                    r && r.call(this, e),
                                    e &&
                                    e._registeredComponents &&
                                    e._registeredComponents.add(s);
                            }),
                                (u._ssrRegister = l))
                            : r &&
                            (l = a
                                ? function () {
                                    r.call(this, this.$root.$options.shadowRoot);
                                }
                                : r),
                        l)
                )
                    if (u.functional) {
                        u._injectStyles = l;
                        var c = u.render;
                        u.render = function (e, t) {
                            return l.call(t), c(e, t);
                        };
                    } else {
                        var d = u.beforeCreate;
                        u.beforeCreate = d ? [].concat(d, l) : [l];
                    }
                return { exports: e, options: u };
            }
            var Oe = be(
                {
                    name: "vue-treeselect--hidden-fields",
                    inject: ["instance"],
                    functional: !0,
                    render: function (e, t) {
                        var n = e,
                            i = t.injections.instance;
                        if (!i.name || i.disabled || !i.hasValue) return null;
                        var r = i.internalValue.map(Se);
                        return (
                            i.multiple && i.joinValues && (r = [r.join(i.delimiter)]),
                            r.map(function (e, t) {
                                return n("input", {
                                    attrs: { type: "hidden", name: i.name },
                                    domProps: { value: e },
                                    key: "hidden-field-" + t
                                });
                            })
                        );
                    }
                },
                void 0,
                void 0,
                !1,
                null,
                null,
                null
            );
            Oe.options.__file = "HiddenFields.vue";
            var _e = Oe.exports,
                xe = n(2),
                Ne = n.n(xe),
                Ee = [oe, ae, le, ue, ce, de, he],
                we = be(
                    {
                        name: "vue-treeselect--input",
                        inject: ["instance"],
                        data: function () {
                            return { inputWidth: 5, value: "" };
                        },
                        computed: {
                            needAutoSize: function () {
                                var e = this.instance;
                                return e.searchable && !e.disabled && e.multiple;
                            },
                            inputStyle: function () {
                                return {
                                    width: this.needAutoSize
                                        ? "".concat(this.inputWidth, "px")
                                        : null
                                };
                            }
                        },
                        watch: {
                            "instance.trigger.searchQuery": function (e) {
                                this.value = e;
                            },
                            value: function () {
                                this.needAutoSize && this.$nextTick(this.updateInputWidth);
                            }
                        },
                        created: function () {
                            this.debouncedCallback = p()(this.updateSearchQuery, 200, {
                                leading: !0,
                                trailing: !0
                            });
                        },
                        methods: {
                            clear: function () {
                                this.onInput({ target: { value: "" } });
                            },
                            focus: function () {
                                this.instance.disabled ||
                                    (this.$refs.input && this.$refs.input.focus());
                            },
                            blur: function () {
                                this.$refs.input && this.$refs.input.blur();
                            },
                            onFocus: function () {
                                var e = this.instance;
                                (e.trigger.isFocused = !0), e.openOnFocus && e.openMenu();
                            },
                            onBlur: function () {
                                var e = this.instance;
                                if (document.activeElement === e.$refs.menu)
                                    return this.focus();
                                (e.trigger.isFocused = !1), e.closeMenu();
                            },
                            onInput: function (e) {
                                var t = e.target.value;
                                (this.value = t)
                                    ? this.debouncedCallback()
                                    : (this.debouncedCallback.cancel(), this.updateSearchQuery());
                            },
                            onKeyDown: function (e) {
                                var t = this.instance,
                                    n = "which" in e ? e.which : e.keyCode;
                                if (!(e.ctrlKey || e.shiftKey || e.altKey || e.metaKey)) {
                                    if (!t.menu.isOpen && P(Ee, n))
                                        return e.preventDefault(), t.openMenu();
                                    switch (n) {
                                        case re:
                                            t.backspaceRemoves &&
                                                !this.value.length &&
                                                t.removeLastValue();
                                            break;
                                        case oe:
                                            tab:
                                            e.preventDefault();
                                            var i = t.getNode(t.menu.current);
                                            if (i.isBranch && t.disableBranchNodes) return;
                                            this.value = "";
                                            t.select(i);
                                            break;
                                        case vr:
                                            e.preventDefault();
                                            var current = t.forest.nodeMap[t.menu.current];
                                            var isInNode = Object.values(t.forest.nodeMap).filter(x => x.label == this.value).length > 0;
                                            if (!t.isInsertable) {
                                                var i = t.getNode(t.menu.current);
                                                if (i.isBranch && t.disableBranchNodes) return;
                                                this.value = "";
                                                t.select(i);
                                                break;
                                            }
                                            else if (isInNode) {
                                                var i = t.getNode(Object.values(t.forest.nodeMap).filter(x => x.label == this.value)[0].id);
                                                t.select(i);
                                            }
                                            else if (t.forest.selectedNodeMap[e.target.value] == undefined && t.forest.nodeMap[e.target.value] == undefined && e.target.value != "") {
                                                t.options.push({
                                                    id: e.target.value,
                                                    label: e.target.value
                                                });
                                                t.forest.selectedNodeIds.push(e.target.value);
                                                t.forest.selectedNodeMap[e.target.value] = !0;
                                                setTimeout(() => {
                                                    var i = t.getNode(e.target.value);
                                                    t.select(i);
                                                }, 1);

                                            }
                                            else if (t.forest.nodeMap[e.target.value] != undefined) {
                                                var i = t.getNode(e.target.value);
                                                t.select(i);
                                            }

                                            this.value = "";

                                            break;
                                        case se:
                                            this.value.length
                                                ? this.clear()
                                                : t.menu.isOpen && t.closeMenu();
                                            break;
                                        case ae:
                                            e.preventDefault(), t.highlightLastOption();
                                            break;
                                        case le:
                                            e.preventDefault(), t.highlightFirstOption();
                                            break;
                                        case ue:
                                            var r = t.getNode(t.menu.current);
                                            r.isBranch && t.shouldExpand(r)
                                                ? (e.preventDefault(), t.toggleExpanded(r))
                                                : !r.isRootNode &&
                                                (r.isLeaf || (r.isBranch && !t.shouldExpand(r))) &&
                                                (e.preventDefault(),
                                                    t.setCurrentHighlightedOption(r.parentNode));
                                            break;
                                        case ce:
                                            e.preventDefault(), t.highlightPrevOption();
                                            break;
                                        case de:
                                            var o = t.getNode(t.menu.current);
                                            o.isBranch &&
                                                !t.shouldExpand(o) &&
                                                (e.preventDefault(), t.toggleExpanded(o));
                                            break;
                                        case he:
                                            e.preventDefault(), t.highlightNextOption();
                                            break;
                                        case fe:
                                            t.deleteRemoves &&
                                                !this.value.length &&
                                                t.removeLastValue();
                                            break;
                                        default:
                                            t.openMenu();
                                    }

                                }

                            },
                            onMouseDown: function (e) {
                                this.value.length && e.stopPropagation();
                            },
                            renderInputContainer: function () {
                                var e = this.$createElement,
                                    t = this.instance,
                                    n = {},
                                    i = [];
                                return (
                                    t.searchable &&
                                    !t.disabled &&
                                    (i.push(this.renderInput()),
                                        this.needAutoSize && i.push(this.renderSizer())),
                                    t.searchable ||
                                    F(n, {
                                        on: {
                                            focus: this.onFocus,
                                            blur: this.onBlur,
                                            keydown: this.onKeyDown
                                        },
                                        ref: "input"
                                    }),
                                    t.searchable ||
                                    t.disabled ||
                                    F(n, { attrs: { tabIndex: t.tabIndex } }),
                                    e(
                                        "div",
                                        Ne()([{ class: "vue-treeselect__input-container" }, n]),
                                        [i]
                                    )
                                );
                            },
                            renderInput: function () {
                                var e = this.$createElement,
                                    t = this.instance;
                                return e("input", {
                                    ref: "input",
                                    class: "vue-treeselect__input",
                                    attrs: {
                                        type: "text",
                                        autocomplete: "off",
                                        tabIndex: t.tabIndex,
                                        value: this.value,
                                        required: t.required && !t.hasValue
                                    },
                                    domProps: { value: this.value },
                                    style: this.inputStyle,
                                    on: {
                                        focus: this.onFocus,
                                        input: this.onInput,
                                        blur: this.onBlur,
                                        keydown: this.onKeyDown,
                                        mousedown: this.onMouseDown
                                    }
                                });
                            },
                            renderSizer: function () {
                                return (0, this.$createElement)(
                                    "div",
                                    { ref: "sizer", class: "vue-treeselect__sizer" },
                                    [this.value]
                                );
                            },
                            updateInputWidth: function () {
                                this.inputWidth = Math.max(
                                    5,
                                    this.$refs.sizer.scrollWidth + 15
                                );
                            },
                            updateSearchQuery: function () {
                                this.instance.trigger.searchQuery = this.value;
                            }
                        },
                        render: function () {
                            return this.renderInputContainer();
                        }
                    },
                    void 0,
                    void 0,
                    !1,
                    null,
                    null,
                    null
                );
            we.options.__file = "Input.vue";
            var Le = we.exports,
                Ce = be(
                    {
                        name: "vue-treeselect--placeholder",
                        inject: ["instance"],
                        render: function () {
                            var e = arguments[0],
                                t = this.instance;
                            return e(
                                "div",
                                {
                                    class: {
                                        "vue-treeselect__placeholder": !0,
                                        "vue-treeselect-helper-zoom-effect-off": !0,
                                        "vue-treeselect-helper-hide":
                                            t.hasValue || t.trigger.searchQuery
                                    }
                                },
                                [t.placeholder]
                            );
                        }
                    },
                    void 0,
                    void 0,
                    !1,
                    null,
                    null,
                    null
                );
            Ce.options.__file = "Placeholder.vue";
            var Me = Ce.exports,
                Ie = be(
                    {
                        name: "vue-treeselect--single-value",
                        inject: ["instance"],
                        methods: {
                            renderSingleValueLabel: function () {
                                var e = this.instance,
                                    t = e.selectedNodes[0],
                                    n = e.$scopedSlots["value-label"];
                                document.getElementById("selectedCategoryId").value = t.id;
                                return n ? n({ node: t }) : t.label;
                            }
                        },
                        render: function () {
                            var e = arguments[0],
                                t = this.instance;
                            return (0, this.$parent.renderValueContainer)([
                                t.hasValue &&
                                !t.trigger.searchQuery &&
                                e("div", { class: "vue-treeselect__single-value" }, [
                                    this.renderSingleValueLabel()
                                ]),
                                e(Me),
                                e(Le, { ref: "input" })
                            ]);
                        }
                    },
                    void 0,
                    void 0,
                    !1,
                    null,
                    null,
                    null
                );
            Ie.options.__file = "SingleValue.vue";
            var De = Ie.exports,
                Te = be(
                    { name: "vue-treeselect--x" },
                    function () {
                        var e = this.$createElement,
                            t = this._self._c || e;
                        return t(
                            "svg",
                            {
                                attrs: {
                                    xmlns: "http://www.w3.org/2000/svg",
                                    viewBox: "0 0 348.333 348.333"
                                }
                            },
                            [
                                t("path", {
                                    attrs: {
                                        d:
                                            "M336.559 68.611L231.016 174.165l105.543 105.549c15.699 15.705 15.699 41.145 0 56.85-7.844 7.844-18.128 11.769-28.407 11.769-10.296 0-20.581-3.919-28.419-11.769L174.167 231.003 68.609 336.563c-7.843 7.844-18.128 11.769-28.416 11.769-10.285 0-20.563-3.919-28.413-11.769-15.699-15.698-15.699-41.139 0-56.85l105.54-105.549L11.774 68.611c-15.699-15.699-15.699-41.145 0-56.844 15.696-15.687 41.127-15.687 56.829 0l105.563 105.554L279.721 11.767c15.705-15.687 41.139-15.687 56.832 0 15.705 15.699 15.705 41.145.006 56.844z"
                                    }
                                })
                            ]
                        );
                    },
                    [],
                    !1,
                    null,
                    null,
                    null
                );
            Te.options.__file = "Delete.vue";
            var $e = Te.exports,
                Ae = be(
                    {
                        name: "vue-treeselect--multi-value-item",
                        inject: ["instance"],
                        props: { node: { type: Object, required: !0 } },
                        methods: {
                            handleMouseDown: d(function () {
                                var e = this.instance,
                                    t = this.node;
                                e.select(t);
                            })
                        },
                        render: function () {
                            var e = arguments[0],
                                t = this.instance,
                                n = this.node,
                                i = {
                                    "vue-treeselect__multi-value-item": !0,
                                    "vue-treeselect__multi-value-item-disabled": n.isDisabled,
                                    "vue-treeselect__multi-value-item-new": n.isNew
                                },
                                r = t.$scopedSlots["value-label"],
                                o = r ? r({ node: n }) : n.label;
                            return e(
                                "div",
                                { class: "vue-treeselect__multi-value-item-container" },
                                [
                                    e(
                                        "div",
                                        { class: i, on: { mousedown: this.handleMouseDown } },
                                        [
                                            e(
                                                "span",
                                                { class: "vue-treeselect__multi-value-label" },
                                                [o]
                                            ),
                                            e(
                                                "span",
                                                {
                                                    class:
                                                        "vue-treeselect__icon vue-treeselect__value-remove"
                                                },
                                                [e($e)]
                                            )
                                        ]
                                    )
                                ]
                            );
                        }
                    },
                    void 0,
                    void 0,
                    !1,
                    null,
                    null,
                    null
                );
            Ae.options.__file = "MultiValueItem.vue";
            var Be = Ae.exports,
                Re = be(
                    {
                        name: "vue-treeselect--multi-value",
                        inject: ["instance"],
                        methods: {
                            renderMultiValueItems: function () {
                                var t = this.$createElement,
                                    e = this.instance;
                                return e.internalValue
                                    .slice(0, e.limit)
                                    .map(e.getNode)
                                    .map(function (e) {
                                        return t(Be, {
                                            key: "multi-value-item-".concat(e.id),
                                            attrs: { node: e }
                                        });
                                    });
                            },
                            renderExceedLimitTip: function () {
                                var e = this.$createElement,
                                    t = this.instance,
                                    n = t.internalValue.length - t.limit;
                                return n <= 0
                                    ? null
                                    : e(
                                        "div",
                                        {
                                            class:
                                                "vue-treeselect__limit-tip vue-treeselect-helper-zoom-effect-off",
                                            key: "exceed-limit-tip"
                                        },
                                        [
                                            e("span", { class: "vue-treeselect__limit-tip-text" }, [
                                                t.limitText(n)
                                            ])
                                        ]
                                    );
                            }
                        },
                        render: function () {
                            var e = arguments[0];
                            return (0, this.$parent.renderValueContainer)(
                                e(
                                    "transition-group",
                                    Ne()([
                                        { class: "vue-treeselect__multi-value" },
                                        {
                                            props: {
                                                tag: "div",
                                                name: "vue-treeselect__multi-value-item--transition",
                                                appear: !0
                                            }
                                        }
                                    ]),
                                    [
                                        this.renderMultiValueItems(),
                                        this.renderExceedLimitTip(),
                                        e(Me, { key: "placeholder" }),
                                        e(Le, { ref: "input", key: "input" })
                                    ]
                                )
                            );
                        }
                    },
                    void 0,
                    void 0,
                    !1,
                    null,
                    null,
                    null
                );
            Re.options.__file = "MultiValue.vue";
            var ze = Re.exports,
                Ve = be(
                    { name: "vue-treeselect--arrow" },
                    function () {
                        var e = this.$createElement,
                            t = this._self._c || e;
                        return t(
                            "svg",
                            {
                                attrs: {
                                    xmlns: "http://www.w3.org/2000/svg",
                                    viewBox: "0 0 292.362 292.362"
                                }
                            },
                            [
                                t("path", {
                                    attrs: {
                                        d:
                                            "M286.935 69.377c-3.614-3.617-7.898-5.424-12.848-5.424H18.274c-4.952 0-9.233 1.807-12.85 5.424C1.807 72.998 0 77.279 0 82.228c0 4.948 1.807 9.229 5.424 12.847l127.907 127.907c3.621 3.617 7.902 5.428 12.85 5.428s9.233-1.811 12.847-5.428L286.935 95.074c3.613-3.617 5.427-7.898 5.427-12.847 0-4.948-1.814-9.229-5.427-12.85z"
                                    }
                                })
                            ]
                        );
                    },
                    [],
                    !1,
                    null,
                    null,
                    null
                );
            Ve.options.__file = "Arrow.vue";
            var ke = Ve.exports,
                Fe = be(
                    {
                        name: "vue-treeselect--control",
                        inject: ["instance"],
                        computed: {
                            shouldShowX: function () {
                                var e = this.instance;
                                return (
                                    e.clearable &&
                                    !e.disabled &&
                                    e.hasValue &&
                                    (this.hasUndisabledValue || e.allowClearingDisabled)
                                );
                            },
                            shouldShowArrow: function () {
                                var e = this.instance;
                                return !e.alwaysOpen || !e.menu.isOpen;
                            },
                            hasUndisabledValue: function () {
                                var t = this.instance;
                                return (
                                    t.hasValue &&
                                    t.internalValue.some(function (e) {
                                        return !t.getNode(e).isDisabled;
                                    })
                                );
                            }
                        },
                        methods: {
                            renderX: function () {
                                var e = this.$createElement,
                                    t = this.instance,
                                    n = t.multiple ? t.clearAllText : t.clearValueText;
                                return this.shouldShowX
                                    ? e(
                                        "div",
                                        {
                                            class: "vue-treeselect__x-container",
                                            attrs: { title: n },
                                            on: { mousedown: this.handleMouseDownOnX }
                                        },
                                        [e($e, { class: "vue-treeselect__x" })]
                                    )
                                    : null;
                            },
                            renderArrow: function () {
                                var e = this.$createElement,
                                    t = {
                                        "vue-treeselect__control-arrow": !0,
                                        "vue-treeselect__control-arrow--rotated": this.instance.menu
                                            .isOpen
                                    };
                                return this.shouldShowArrow
                                    ? e(
                                        "div",
                                        {
                                            class: "vue-treeselect__control-arrow-container",
                                            on: { mousedown: this.handleMouseDownOnArrow }
                                        },
                                        [e(ke, { class: t })]
                                    )
                                    : null;
                            },
                            handleMouseDownOnX: d(function (e) {
                                e.stopPropagation(), e.preventDefault();
                                var t = this.instance,
                                    n = t.beforeClearAll(),
                                    i = function (e) {
                                        e && t.clear();
                                    };
                                M()(n)
                                    ? n.then(i)
                                    : setTimeout(function () {
                                        return i(n);
                                    }, 0);
                            }),
                            handleMouseDownOnArrow: d(function (e) {
                                e.preventDefault(), e.stopPropagation();
                                var t = this.instance;
                                t.focusInput(), t.toggleMenu();
                            }),
                            renderValueContainer: function (e) {
                                return (0, this.$createElement)(
                                    "div",
                                    { class: "vue-treeselect__value-container" },
                                    [e]
                                );
                            }
                        },
                        render: function () {
                            var e = arguments[0],
                                t = this.instance,
                                n = t.single ? De : ze;
                            return e(
                                "div",
                                {
                                    class: "vue-treeselect__control",
                                    on: { mousedown: t.handleMouseDown }
                                },
                                [
                                    e(n, { ref: "value-container" }),
                                    this.renderX(),
                                    this.renderArrow()
                                ]
                            );
                        }
                    },
                    void 0,
                    void 0,
                    !1,
                    null,
                    null,
                    null
                );
            Fe.options.__file = "Control.vue";
            var je = Fe.exports,
                He = be(
                    {
                        name: "vue-treeselect--tip",
                        functional: !0,
                        props: {
                            type: { type: String, required: !0 },
                            icon: { type: String, required: !0 }
                        },
                        render: function (e, t) {
                            var n = e,
                                i = t.props,
                                r = t.children;
                            return n(
                                "div",
                                {
                                    class: "vue-treeselect__tip vue-treeselect__".concat(
                                        i.type,
                                        "-tip"
                                    )
                                },
                                [
                                    n("div", { class: "vue-treeselect__icon-container" }, [
                                        n("span", { class: "vue-treeselect__icon-".concat(i.icon) })
                                    ]),
                                    n(
                                        "span",
                                        {
                                            class: "vue-treeselect__tip-text vue-treeselect__".concat(
                                                i.type,
                                                "-tip-text"
                                            )
                                        },
                                        [r]
                                    )
                                ]
                            );
                        }
                    },
                    void 0,
                    void 0,
                    !1,
                    null,
                    null,
                    null
                );
            He.options.__file = "Tip.vue";
            var Pe,
                We,
                Qe,
                qe = He.exports,
                Ke = {
                    name: "vue-treeselect--option",
                    inject: ["instance"],
                    props: { node: { type: Object, required: !0 } },
                    computed: {
                        shouldExpand: function () {
                            var e = this.instance,
                                t = this.node;
                            return t.isBranch && e.shouldExpand(t);
                        },
                        shouldShow: function () {
                            var e = this.instance,
                                t = this.node;
                            return e.shouldShowOptionInMenu(t);
                        }
                    },
                    methods: {
                        renderOption: function () {
                            var e = this.$createElement,
                                t = this.instance,
                                n = this.node;
                            return e(
                                "div",
                                {
                                    class: {
                                        "vue-treeselect__option": !0,
                                        "vue-treeselect__option--disabled": n.isDisabled,
                                        "vue-treeselect__option--selected": t.isSelected(n),
                                        "vue-treeselect__option--highlight": n.isHighlighted,
                                        "vue-treeselect__option--matched":
                                            t.localSearch.active && n.isMatched,
                                        "vue-treeselect__option--hide": !this.shouldShow
                                    },
                                    on: { mouseenter: this.handleMouseEnterOption },
                                    attrs: { "data-id": n.id }
                                },
                                [
                                    this.renderArrow(),
                                    this.renderLabelContainer([
                                        this.renderCheckboxContainer([this.renderCheckbox()]),
                                        this.renderLabel()
                                    ])
                                ]
                            );
                        },
                        renderSubOptionsList: function () {
                            var e = this.$createElement;
                            return this.shouldExpand
                                ? e("div", { class: "vue-treeselect__list" }, [
                                    this.renderSubOptions(),
                                    this.renderNoChildrenTip(),
                                    this.renderLoadingChildrenTip(),
                                    this.renderLoadingChildrenErrorTip()
                                ])
                                : null;
                        },
                        renderArrow: function () {
                            var e = this.$createElement,
                                t = this.instance,
                                n = this.node;
                            if (t.shouldFlattenOptions && this.shouldShow) return null;
                            if (n.isBranch) {
                                var i = {
                                    "vue-treeselect__option-arrow": !0,
                                    "vue-treeselect__option-arrow--rotated": this.shouldExpand
                                };
                                return e(
                                    "div",
                                    {
                                        class: "vue-treeselect__option-arrow-container",
                                        on: { mousedown: this.handleMouseDownOnArrow }
                                    },
                                    [
                                        e(
                                            "transition",
                                            {
                                                props: {
                                                    name: "vue-treeselect__option-arrow--prepare",
                                                    appear: !0
                                                }
                                            },
                                            [e(ke, { class: i })]
                                        )
                                    ]
                                );
                            }
                            return t.hasBranchNodes
                                ? (Pe ||
                                    (Pe = e(
                                        "div",
                                        { class: "vue-treeselect__option-arrow-placeholder" },
                                        [" "]
                                    )),
                                    Pe)
                                : null;
                        },
                        renderLabelContainer: function (e) {
                            return (0, this.$createElement)(
                                "div",
                                {
                                    class: "vue-treeselect__label-container",
                                    on: { mousedown: this.handleMouseDownOnLabelContainer }
                                },
                                [e]
                            );
                        },
                        renderCheckboxContainer: function (e) {
                            var t = this.$createElement,
                                n = this.instance,
                                i = this.node;
                            return n.single
                                ? null
                                : n.disableBranchNodes && i.isBranch
                                    ? null
                                    : t("div", { class: "vue-treeselect__checkbox-container" }, [
                                        e
                                    ]);
                        },
                        renderCheckbox: function () {
                            var e = this.$createElement,
                                t = this.instance,
                                n = this.node,
                                i = t.forest.checkedStateMap[n.id],
                                r = {
                                    "vue-treeselect__checkbox": !0,
                                    "vue-treeselect__checkbox--checked": 2 === i,
                                    "vue-treeselect__checkbox--indeterminate": 1 === i,
                                    "vue-treeselect__checkbox--unchecked": 0 === i,
                                    "vue-treeselect__checkbox--disabled": n.isDisabled
                                };
                            return (
                                We || (We = e("span", { class: "vue-treeselect__check-mark" })),
                                Qe || (Qe = e("span", { class: "vue-treeselect__minus-mark" })),
                                e("span", { class: r }, [We, Qe])
                            );
                        },
                        renderLabel: function () {
                            var e = this.$createElement,
                                t = this.instance,
                                n = this.node,
                                i =
                                    n.isBranch &&
                                    (t.localSearch.active
                                        ? t.showCountOnSearchComputed
                                        : t.showCount),
                                r = i
                                    ? t.localSearch.active
                                        ? t.localSearch.countMap[n.id][t.showCountOf]
                                        : n.count[t.showCountOf]
                                    : NaN,
                                o = "vue-treeselect__label",
                                s = "vue-treeselect__count",
                                a = t.$scopedSlots["option-label"];
                            return a
                                ? a({
                                    node: n,
                                    shouldShowCount: i,
                                    count: r,
                                    labelClassName: o,
                                    countClassName: s
                                })
                                : e("label", { class: o }, [
                                    n.label,
                                    i && e("span", { class: s }, ["(", r, ")"])
                                ]);
                        },
                        renderSubOptions: function () {
                            var t = this.$createElement,
                                e = this.node;
                            return e.childrenStates.isLoaded
                                ? e.children.map(function (e) {
                                    return t(Ke, { attrs: { node: e }, key: e.id });
                                })
                                : null;
                        },
                        renderNoChildrenTip: function () {
                            var e = this.$createElement,
                                t = this.instance,
                                n = this.node;
                            return !n.childrenStates.isLoaded || n.children.length
                                ? null
                                : e(qe, { attrs: { type: "no-children", icon: "warning" } }, [
                                    t.noChildrenText
                                ]);
                        },
                        renderLoadingChildrenTip: function () {
                            var e = this.$createElement,
                                t = this.instance;
                            return this.node.childrenStates.isLoading
                                ? e(qe, { attrs: { type: "loading", icon: "loader" } }, [
                                    t.loadingText
                                ])
                                : null;
                        },
                        renderLoadingChildrenErrorTip: function () {
                            var e = this.$createElement,
                                t = this.instance,
                                n = this.node;
                            return n.childrenStates.loadingError
                                ? e(qe, { attrs: { type: "error", icon: "error" } }, [
                                    n.childrenStates.loadingError,
                                    e(
                                        "a",
                                        {
                                            class: "vue-treeselect__retry",
                                            attrs: { title: t.retryTitle },
                                            on: { mousedown: this.handleMouseDownOnRetry }
                                        },
                                        [t.retryText]
                                    )
                                ])
                                : null;
                        },
                        handleMouseEnterOption: function (e) {
                            var t = this.instance,
                                n = this.node;
                            e.target === e.currentTarget &&
                                t.setCurrentHighlightedOption(n, !1);
                        },
                        handleMouseDownOnArrow: d(function () {
                            var e = this.instance,
                                t = this.node;
                            e.toggleExpanded(t);
                        }),
                        handleMouseDownOnLabelContainer: d(function () {
                            var e = this.instance,
                                t = this.node;
                            t.isBranch && e.disableBranchNodes
                                ? e.toggleExpanded(t)
                                : e.select(t);
                        }),
                        handleMouseDownOnRetry: d(function () {
                            var e = this.instance,
                                t = this.node;
                            e.loadChildrenOptions(t);
                        })
                    },
                    render: function () {
                        var e = arguments[0],
                            t = this.node,
                            n = this.instance.shouldFlattenOptions ? 0 : t.level;
                        return e(
                            "div",
                            {
                                class: E()(
                                    { "vue-treeselect__list-item": !0 },
                                    "vue-treeselect__indent-level-".concat(n),
                                    !0
                                )
                            },
                            [
                                this.renderOption(),
                                e(
                                    "transition",
                                    { props: { name: "vue-treeselect__list--transition" } },
                                    [this.renderSubOptionsList()]
                                )
                            ]
                        );
                    }
                },
                Xe = be(Ke, void 0, void 0, !1, null, null, null);
            Xe.options.__file = "Option.vue";
            var Ue = Xe.exports,
                Ye = { top: "top", bottom: "bottom", above: "top", below: "bottom" },
                Je = be(
                    {
                        name: "vue-treeselect--menu",
                        inject: ["instance"],
                        computed: {
                            menuStyle: function () {
                                return { maxHeight: this.instance.maxHeight + "px" };
                            },
                            menuContainerStyle: function () {
                                var e = this.instance;
                                return { zIndex: e.appendToBody ? null : e.zIndex };
                            }
                        },
                        watch: {
                            "instance.menu.isOpen": function (e) {
                                e ? this.$nextTick(this.onMenuOpen) : this.onMenuClose();
                            },
                            "instance.forest.selectedNodeIds": function (news, olds) {
                                if (this.instance.multiple)
                                    document.getElementById(this.instance.field).value = JSON.stringify(this.instance.forest.selectedNodeIds);
                            }
                        },
                        created: function () {
                            (this.menuSizeWatcher = null),
                                (this.menuResizeAndScrollEventListeners = null);
                        },
                        mounted: function () {
                            this.instance.menu.isOpen && this.$nextTick(this.onMenuOpen);
                        },
                        destroyed: function () {
                            this.onMenuClose();
                        },
                        methods: {
                            renderMenu: function () {
                                var e = this.$createElement,
                                    t = this.instance;
                                return t.menu.isOpen
                                    ? e(
                                        "div",
                                        {
                                            ref: "menu",
                                            class: "vue-treeselect__menu",
                                            on: { mousedown: t.handleMouseDown },
                                            style: this.menuStyle
                                        },
                                        [
                                            t.async
                                                ? this.renderAsyncSearchMenuInner()
                                                : t.localSearch.active
                                                    ? this.renderLocalSearchMenuInner()
                                                    : this.renderNormalMenuInner()
                                        ]
                                    )
                                    : null;
                            },
                            renderNormalMenuInner: function () {
                                var e = this.instance;
                                return e.rootOptionsStates.isLoading
                                    ? this.renderLoadingOptionsTip()
                                    : e.rootOptionsStates.loadingError
                                        ? this.renderLoadingRootOptionsErrorTip()
                                        : e.rootOptionsStates.isLoaded &&
                                            0 === e.forest.normalizedOptions.length
                                            ? this.renderNoAvailableOptionsTip()
                                            : this.renderOptionList();
                            },
                            renderLocalSearchMenuInner: function () {
                                var e = this.instance;
                                return e.rootOptionsStates.isLoading
                                    ? this.renderLoadingOptionsTip()
                                    : e.rootOptionsStates.loadingError
                                        ? this.renderLoadingRootOptionsErrorTip()
                                        : e.rootOptionsStates.isLoaded &&
                                            0 === e.forest.normalizedOptions.length
                                            ? this.renderNoAvailableOptionsTip()
                                            : e.localSearch.noResults
                                                ? this.renderNoResultsTip()
                                                : this.renderOptionList();
                            },
                            renderAsyncSearchMenuInner: function () {
                                var e = this.instance,
                                    t = e.getRemoteSearchEntry(),
                                    n = "" === e.trigger.searchQuery && !e.defaultOptions,
                                    i = !n && (t.isLoaded && 0 === t.options.length);
                                return n
                                    ? this.renderSearchPromptTip()
                                    : t.isLoading
                                        ? this.renderLoadingOptionsTip()
                                        : t.loadingError
                                            ? this.renderAsyncSearchLoadingErrorTip()
                                            : i
                                                ? this.renderNoResultsTip()
                                                : this.renderOptionList();
                            },
                            renderOptionList: function () {
                                var t = this.$createElement,
                                    e = this.instance;
                                return t("div", { class: "vue-treeselect__list" }, [
                                    e.forest.normalizedOptions.map(function (e) {
                                        return t(Ue, { attrs: { node: e }, key: e.id });
                                    })
                                ]);
                            },
                            renderSearchPromptTip: function () {
                                var e = this.$createElement,
                                    t = this.instance;
                                return e(
                                    qe,
                                    { attrs: { type: "search-prompt", icon: "warning" } },
                                    [t.searchPromptText]
                                );
                            },
                            renderLoadingOptionsTip: function () {
                                var e = this.$createElement,
                                    t = this.instance;
                                return e(qe, { attrs: { type: "loading", icon: "loader" } }, [
                                    t.loadingText
                                ]);
                            },
                            renderLoadingRootOptionsErrorTip: function () {
                                var e = this.$createElement,
                                    t = this.instance;
                                return e(qe, { attrs: { type: "error", icon: "error" } }, [
                                    t.rootOptionsStates.loadingError,
                                    e(
                                        "a",
                                        {
                                            class: "vue-treeselect__retry",
                                            on: { click: t.loadRootOptions },
                                            attrs: { title: t.retryTitle }
                                        },
                                        [t.retryText]
                                    )
                                ]);
                            },
                            renderAsyncSearchLoadingErrorTip: function () {
                                var e = this.$createElement,
                                    t = this.instance,
                                    n = t.getRemoteSearchEntry();
                                return e(qe, { attrs: { type: "error", icon: "error" } }, [
                                    n.loadingError,
                                    e(
                                        "a",
                                        {
                                            class: "vue-treeselect__retry",
                                            on: { click: t.handleRemoteSearch },
                                            attrs: { title: t.retryTitle }
                                        },
                                        [t.retryText]
                                    )
                                ]);
                            },
                            renderNoAvailableOptionsTip: function () {
                                var e = this.$createElement,
                                    t = this.instance;
                                return e(
                                    qe,
                                    { attrs: { type: "no-options", icon: "warning" } },
                                    [t.noOptionsText]
                                );
                            },
                            renderNoResultsTip: function () {
                                var e = this.$createElement,
                                    t = this.instance;
                                return e(
                                    qe,
                                    { attrs: { type: "no-results", icon: "warning" } },
                                    [t.noResultsText]
                                );
                            },
                            onMenuOpen: function () {
                                this.adjustMenuOpenDirection(),
                                    this.setupMenuSizeWatcher(),
                                    this.setupMenuResizeAndScrollEventListeners();
                            },
                            onMenuClose: function () {
                                this.removeMenuSizeWatcher(),
                                    this.removeMenuResizeAndScrollEventListeners();
                            },
                            adjustMenuOpenDirection: function () {
                                var e = this.instance;
                                if (e.menu.isOpen) {
                                    var t = e.getMenu(),
                                        n = e.getControl(),
                                        i = t.getBoundingClientRect(),
                                        r = n.getBoundingClientRect(),
                                        o = i.height,
                                        s = window.innerHeight,
                                        a = r.top,
                                        l = o + 40 < window.innerHeight - r.bottom,
                                        u = o + 40 < a;
                                    (0 <= r.top && r.top <= s) || (r.top < 0 && 0 < r.bottom)
                                        ? "auto" !== e.openDirection
                                            ? (e.menu.placement = Ye[e.openDirection])
                                            : (e.menu.placement = l || !u ? "bottom" : "top")
                                        : e.closeMenu();
                                }
                            },
                            setupMenuSizeWatcher: function () {
                                var e = this.instance.getMenu();
                                this.menuSizeWatcher ||
                                    (this.menuSizeWatcher = {
                                        remove: O(e, this.adjustMenuOpenDirection)
                                    });
                            },
                            setupMenuResizeAndScrollEventListeners: function () {
                                var e = this.instance.getControl();
                                this.menuResizeAndScrollEventListeners ||
                                    (this.menuResizeAndScrollEventListeners = {
                                        remove: _(e, this.adjustMenuOpenDirection)
                                    });
                            },
                            removeMenuSizeWatcher: function () {
                                this.menuSizeWatcher &&
                                    (this.menuSizeWatcher.remove(),
                                        (this.menuSizeWatcher = null));
                            },
                            removeMenuResizeAndScrollEventListeners: function () {
                                this.menuResizeAndScrollEventListeners &&
                                    (this.menuResizeAndScrollEventListeners.remove(),
                                        (this.menuResizeAndScrollEventListeners = null));
                            }
                        },
                        render: function () {
                            var e = arguments[0];
                            return e(
                                "div",
                                {
                                    ref: "menu-container",
                                    class: "vue-treeselect__menu-container",
                                    style: this.menuContainerStyle
                                },
                                [
                                    e(
                                        "transition",
                                        { attrs: { name: "vue-treeselect__menu--transition" } },
                                        [this.renderMenu()]
                                    )
                                ]
                            );
                        }
                    },
                    void 0,
                    void 0,
                    !1,
                    null,
                    null,
                    null
                );
            Je.options.__file = "Menu.vue";
            var Ge,
                Ze = Je.exports,
                et = n(18),
                tt = n.n(et),
                nt = {
                    name: "vue-treeselect--portal-target",
                    inject: ["instance"],
                    watch: {
                        "instance.menu.isOpen": function (e) {
                            e ? this.setupHandlers() : this.removeHandlers();
                        },
                        "instance.menu.placement": function () {
                            this.updateMenuContainerOffset();
                        }
                    },
                    created: function () {
                        (this.controlResizeAndScrollEventListeners = null),
                            (this.controlSizeWatcher = null);
                    },
                    mounted: function () {
                        this.instance.menu.isOpen && this.setupHandlers();
                    },
                    methods: {
                        setupHandlers: function () {
                            this.updateWidth(),
                                this.updateMenuContainerOffset(),
                                this.setupControlResizeAndScrollEventListeners(),
                                this.setupControlSizeWatcher();
                        },
                        removeHandlers: function () {
                            this.removeControlResizeAndScrollEventListeners(),
                                this.removeControlSizeWatcher();
                        },
                        setupControlResizeAndScrollEventListeners: function () {
                            var e = this.instance.getControl();
                            this.controlResizeAndScrollEventListeners ||
                                (this.controlResizeAndScrollEventListeners = {
                                    remove: _(e, this.updateMenuContainerOffset)
                                });
                        },
                        setupControlSizeWatcher: function () {
                            var e = this,
                                t = this.instance.getControl();
                            this.controlSizeWatcher ||
                                (this.controlSizeWatcher = {
                                    remove: O(t, function () {
                                        e.updateWidth(), e.updateMenuContainerOffset();
                                    })
                                });
                        },
                        removeControlResizeAndScrollEventListeners: function () {
                            this.controlResizeAndScrollEventListeners &&
                                (this.controlResizeAndScrollEventListeners.remove(),
                                    (this.controlResizeAndScrollEventListeners = null));
                        },
                        removeControlSizeWatcher: function () {
                            this.controlSizeWatcher &&
                                (this.controlSizeWatcher.remove(),
                                    (this.controlSizeWatcher = null));
                        },
                        updateWidth: function () {
                            var e = this.instance,
                                t = this.$el,
                                n = e.getControl().getBoundingClientRect();
                            t.style.width = n.width + "px";
                        },
                        updateMenuContainerOffset: function () {
                            var e = this.instance,
                                t = e.getControl(),
                                n = this.$el,
                                i = t.getBoundingClientRect(),
                                r = n.getBoundingClientRect(),
                                o = "bottom" === e.menu.placement ? i.height : 0,
                                s = Math.round(i.left - r.left) + "px",
                                a = Math.round(i.top - r.top + o) + "px";
                            this.$refs.menu.$refs["menu-container"].style[
                                W(
                                    [
                                        "transform",
                                        "webkitTransform",
                                        "MozTransform",
                                        "msTransform"
                                    ],
                                    function (e) {
                                        return e in document.body.style;
                                    }
                                )
                            ] = "translate(".concat(s, ", ").concat(a, ")");
                        }
                    },
                    render: function () {
                        var e = arguments[0],
                            t = this.instance;
                        return e(
                            "div",
                            {
                                class: ["vue-treeselect__portal-target", t.wrapperClass],
                                style: { zIndex: t.zIndex },
                                attrs: { "data-instance-id": t.getInstanceId() }
                            },
                            [e(Ze, { ref: "menu" })]
                        );
                    },
                    destroyed: function () {
                        this.removeHandlers();
                    }
                },
                it = be(
                    {
                        name: "vue-treeselect--menu-portal",
                        created: function () {
                            this.portalTarget = null;
                        },
                        mounted: function () {
                            this.setup();
                        },
                        destroyed: function () {
                            this.teardown();
                        },
                        methods: {
                            setup: function () {
                                var e = document.createElement("div");
                                document.body.appendChild(e),
                                    (this.portalTarget = new tt.a(
                                        w()({ el: e, parent: this }, nt)
                                    ));
                            },
                            teardown: function () {
                                document.body.removeChild(this.portalTarget.$el),
                                    (this.portalTarget.$el.innerHTML = ""),
                                    this.portalTarget.$destroy(),
                                    (this.portalTarget = null);
                            }
                        },
                        render: function () {
                            var e = arguments[0];
                            return (
                                Ge ||
                                (Ge = e("div", {
                                    class: "vue-treeselect__menu-placeholder"
                                })),
                                Ge
                            );
                        }
                    },
                    void 0,
                    void 0,
                    !1,
                    null,
                    null,
                    null
                );
            it.options.__file = "MenuPortal.vue";
            var rt = it.exports,
                ot = be(
                    {
                        name: "vue-treeselect",
                        mixins: [ye],
                        computed: {
                            wrapperClass: function () {
                                return {
                                    "vue-treeselect": !0,
                                    "vue-treeselect--single": this.single,
                                    "vue-treeselect--multi": this.multiple,
                                    "vue-treeselect--searchable": this.searchable,
                                    "vue-treeselect--disabled": this.disabled,
                                    "vue-treeselect--focused": this.trigger.isFocused,
                                    "vue-treeselect--has-value": this.hasValue,
                                    "vue-treeselect--open": this.menu.isOpen,
                                    "vue-treeselect--open-above": "top" === this.menu.placement,
                                    "vue-treeselect--open-below":
                                        "bottom" === this.menu.placement,
                                    "vue-treeselect--branch-nodes-disabled": this
                                        .disableBranchNodes,
                                    "vue-treeselect--append-to-body": this.appendToBody
                                };
                            }
                        },
                        render: function () {
                            var e = arguments[0];
                            return e("div", { ref: "wrapper", class: this.wrapperClass }, [
                                e(_e),
                                e(je, { ref: "control" }),
                                this.appendToBody
                                    ? e(rt, { ref: "portal" })
                                    : e(Ze, { ref: "menu" })
                            ]);
                        }
                    },
                    void 0,
                    void 0,
                    !1,
                    null,
                    null,
                    null
                );
            ot.options.__file = "Treeselect.vue";
            var st = ot.exports;
            n(36);
            n.d(t, "__esModule", function () {
                return at;
            }),
                n.d(t, "VERSION", function () {
                    return lt;
                }),
                n.d(t, "Treeselect", function () {
                    return st;
                }),
                n.d(t, "treeselectMixin", function () {
                    return ye;
                }),
                n.d(t, "LOAD_ROOT_OPTIONS", function () {
                    return J;
                }),
                n.d(t, "LOAD_CHILDREN_OPTIONS", function () {
                    return G;
                }),
                n.d(t, "ASYNC_SEARCH", function () {
                    return Z;
                });
            t.default = st;
            var at = !0,
                lt = "0.0.38";
        }
    ]);
});
