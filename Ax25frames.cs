// This is a generated file! Please edit source .ksy file and use kaitai-struct-compiler to rebuild
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaitai;

namespace tlm_cas5_plugin
{

    /// <summary>
    /// :field dest_callsign: ax25_frame.ax25_header.dest_callsign_raw.callsign_ror.callsign
    /// :field src_callsign: ax25_frame.ax25_header.src_callsign_raw.callsign_ror.callsign
    /// :field src_ssid: ax25_frame.ax25_header.src_ssid_raw.ssid
    /// :field dest_ssid: ax25_frame.ax25_header.dest_ssid_raw.ssid
    /// :field ctl: ax25_frame.ax25_header.ctl
    /// :field pid: ax25_frame.payload.pid
    /// </summary>
    public partial class Ax25frames : KaitaiStruct
    {
        public static Ax25frames FromFile(string fileName)
        {
            return new Ax25frames(new KaitaiStream(fileName));
        }

        public Ax25frames(KaitaiStream p__io, KaitaiStruct p__parent = null, Ax25frames p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _ax25Frame = new Ax25Frame(m_io, this, m_root);
        }
        public partial class Ax25Frame : KaitaiStruct
        {
            public static Ax25Frame FromFile(string fileName)
            {
                return new Ax25Frame(new KaitaiStream(fileName));
            }

            public Ax25Frame(KaitaiStream p__io, Ax25frames p__parent = null, Ax25frames p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _ax25Header = new Ax25Header(m_io, this, m_root);
                switch ((Ax25Header.Ctl & 19)) {
                case 0: {
                    _payload = new IFrame(m_io, this, m_root);
                    break;
                }
                case 3: {
                    _payload = new UiFrame(m_io, this, m_root);
                    break;
                }
                case 19: {
                    _payload = new UiFrame(m_io, this, m_root);
                    break;
                }
                case 16: {
                    _payload = new IFrame(m_io, this, m_root);
                    break;
                }
                case 18: {
                    _payload = new IFrame(m_io, this, m_root);
                    break;
                }
                case 2: {
                    _payload = new IFrame(m_io, this, m_root);
                    break;
                }
                }
            }
            private Ax25Header _ax25Header;
            private KaitaiStruct _payload;
            private Ax25frames m_root;
            private Ax25frames m_parent;
            public Ax25Header Ax25Header { get { return _ax25Header; } }
            public KaitaiStruct payload { get { return _payload; } }
            public Ax25frames M_Root { get { return m_root; } }
            public Ax25frames M_Parent { get { return m_parent; } }
        }
        public partial class Ax25Header : KaitaiStruct
        {
            public static Ax25Header FromFile(string fileName)
            {
                return new Ax25Header(new KaitaiStream(fileName));
            }

            public Ax25Header(KaitaiStream p__io, Ax25frames.Ax25Frame p__parent = null, Ax25frames p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _destCallsignRaw = new CallsignRaw(m_io, this, m_root);
                _destSsidRaw = new SsidMask(m_io, this, m_root);
                _srcCallsignRaw = new CallsignRaw(m_io, this, m_root);
                _srcSsidRaw = new SsidMask(m_io, this, m_root);
                _ctl = m_io.ReadU1();
            }
            private CallsignRaw _destCallsignRaw;
            private SsidMask _destSsidRaw;
            private CallsignRaw _srcCallsignRaw;
            private SsidMask _srcSsidRaw;
            private byte _ctl;
            private Ax25frames m_root;
            private Ax25frames.Ax25Frame m_parent;
            public CallsignRaw DestCallsignRaw { get { return _destCallsignRaw; } }
            public SsidMask DestSsidRaw { get { return _destSsidRaw; } }
            public CallsignRaw SrcCallsignRaw { get { return _srcCallsignRaw; } }
            public SsidMask SrcSsidRaw { get { return _srcSsidRaw; } }
            public byte Ctl { get { return _ctl; } }
            public Ax25frames M_Root { get { return m_root; } }
            public Ax25frames.Ax25Frame M_Parent { get { return m_parent; } }
        }
        public partial class UiFrame : KaitaiStruct
        {
            public static UiFrame FromFile(string fileName)
            {
                return new UiFrame(new KaitaiStream(fileName));
            }

            public UiFrame(KaitaiStream p__io, Ax25frames.Ax25Frame p__parent = null, Ax25frames p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _pid = m_io.ReadU1();
                _ax25Info = m_io.ReadBytesFull();
            }
            private byte _pid;
            private byte[] _ax25Info;
            private Ax25frames m_root;
            private Ax25frames.Ax25Frame m_parent;
            public byte Pid { get { return _pid; } }
            public byte[] Ax25Info { get { return _ax25Info; } }
            public Ax25frames M_Root { get { return m_root; } }
            public Ax25frames.Ax25Frame M_Parent { get { return m_parent; } }
        }
        public partial class Callsign : KaitaiStruct
        {
            public static Callsign FromFile(string fileName)
            {
                return new Callsign(new KaitaiStream(fileName));
            }

            public Callsign(KaitaiStream p__io, Ax25frames.CallsignRaw p__parent = null, Ax25frames p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _callsign = System.Text.Encoding.GetEncoding("ASCII").GetString(m_io.ReadBytes(6));
            }
            private string _callsign;
            private Ax25frames m_root;
            private Ax25frames.CallsignRaw m_parent;
            public string callsign { get { return _callsign; } }
            public Ax25frames M_Root { get { return m_root; } }
            public Ax25frames.CallsignRaw M_Parent { get { return m_parent; } }
        }
        public partial class IFrame : KaitaiStruct
        {
            public static IFrame FromFile(string fileName)
            {
                return new IFrame(new KaitaiStream(fileName));
            }

            public IFrame(KaitaiStream p__io, Ax25frames.Ax25Frame p__parent = null, Ax25frames p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _pid = m_io.ReadU1();
                _ax25Info = m_io.ReadBytesFull();
            }
            private byte _pid;
            private byte[] _ax25Info;
            private Ax25frames m_root;
            private Ax25frames.Ax25Frame m_parent;
            public byte Pid { get { return _pid; } }
            public byte[] Ax25Info { get { return _ax25Info; } }
            public Ax25frames M_Root { get { return m_root; } }
            public Ax25frames.Ax25Frame M_Parent { get { return m_parent; } }
        }
        public partial class SsidMask : KaitaiStruct
        {
            public static SsidMask FromFile(string fileName)
            {
                return new SsidMask(new KaitaiStream(fileName));
            }

            public SsidMask(KaitaiStream p__io, Ax25frames.Ax25Header p__parent = null, Ax25frames p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                f_ssid = false;
                _read();
            }
            private void _read()
            {
                _ssidMask = m_io.ReadU1();
            }
            private bool f_ssid;
            private int _ssid;
            public int Ssid
            {
                get
                {
                    if (f_ssid)
                        return _ssid;
                    _ssid = (int) (((ssidmask & 15) >> 1));
                    f_ssid = true;
                    return _ssid;
                }
            }
            private byte _ssidMask;
            private Ax25frames m_root;
            private Ax25frames.Ax25Header m_parent;
            public byte ssidmask { get { return _ssidMask; } }
            public Ax25frames M_Root { get { return m_root; } }
            public Ax25frames.Ax25Header M_Parent { get { return m_parent; } }
        }
        public partial class CallsignRaw : KaitaiStruct
        {
            public static CallsignRaw FromFile(string fileName)
            {
                return new CallsignRaw(new KaitaiStream(fileName));
            }

            public CallsignRaw(KaitaiStream p__io, Ax25frames.Ax25Header p__parent = null, Ax25frames p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                __raw__raw_callsignRor = m_io.ReadBytes(6);
                __raw_callsignRor = m_io.ProcessRotateLeft(__raw__raw_callsignRor, 8 - (1), 1);
                var io___raw_callsignRor = new KaitaiStream(__raw_callsignRor);
                _callsignRor = new Callsign(io___raw_callsignRor, this, m_root);
            }
            private Callsign _callsignRor;
            private Ax25frames m_root;
            private Ax25frames.Ax25Header m_parent;
            private byte[] __raw_callsignRor;
            private byte[] __raw__raw_callsignRor;
            public Callsign CallsignRor { get { return _callsignRor; } }
            public Ax25frames M_Root { get { return m_root; } }
            public Ax25frames.Ax25Header M_Parent { get { return m_parent; } }
            public byte[] M_RawCallsignRor { get { return __raw_callsignRor; } }
            public byte[] M_RawM_RawCallsignRor { get { return __raw__raw_callsignRor; } }
        }
        private Ax25Frame _ax25Frame;
        private Ax25frames m_root;
        private KaitaiStruct m_parent;

        /// <remarks>
        /// Reference: <a href="https://www.tapr.org/pub_ax25.html">Source</a>
        /// </remarks>
        public Ax25Frame ax25frame { get { return _ax25Frame; } }
        public Ax25frames M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
